# 使用 Trusted Publishing 发布 NuGet 包

nuget.org 已不再推荐长期 API key，改用 Trusted Publishing：CI 用 GitHub OIDC token 换取 1 小时有效的临时 key 再推送，仓库里不需要保存任何密钥。

## 1. 发布元数据

包版本由环境变量注入，`release.env` 是唯一来源，本地打包脚本和 CI 工作流都 source 它：

```bash
export PACKAGE_VERSION="10.0.5"
export PACKAGE_RELEASE_NOTES="Update Microsoft packages to 10.0.15"
export PACKAGE_TAGS="nhibernate, aspnetcore, identity"
export PACKAGE_PROJECT_URL="https://github.com/nhibernate/NHibernate.AspNetCore.Identity"
```

`csproj` 里引用这些变量：

```xml
<PackageVersion>$(PACKAGE_VERSION)</PackageVersion>
<PackageReleaseNotes>$(PACKAGE_RELEASE_NOTES)</PackageReleaseNotes>
<PackageTags>$(PACKAGE_TAGS)</PackageTags>
<PackageProjectUrl>$(PACKAGE_PROJECT_URL)</PackageProjectUrl>
```

变量必须 `export`，否则 `dotnet pack` 读不到，会退化成默认版本 `1.0.0`。

## 2. nuget.org 策略

登录 nuget.org → 用户菜单 → **Trusted Publishing** → Create：

| 字段 | 值 |
| --- | --- |
| Package owner | 包的 owner 账号或组织 |
| Repository Owner | `nhibernate` |
| Repository | `NHibernate.AspNetCore.Identity` |
| Workflow File | `publish-nuget.yml`（只填文件名） |
| Environment | 留空 |
| Scopes | Push new packages and package versions |
| Glob | `NHibernate.AspNetCore.Identity` |

Workflow File 和 Environment 必须与工作流完全一致，否则换取 key 时报 `No matching trust policy`。

## 3. GitHub 变量

Settings → Secrets and variables → Actions → Variables → New repository variable：

| 名称 | 值 |
| --- | --- |
| `NUGET_USER` | nuget.org 的用户名（profile name，不是邮箱） |

## 4. 工作流

`.github/workflows/publish-nuget.yml` 由标签触发：校验标签与 `release.env` 一致 → `dotnet pack` → `NuGet/login` 换取临时 key → `dotnet nuget push`。

要点：

- `id-token: write` 只给发布 job；
- `NuGet/login` 必须放在 `dotnet nuget push` 之前，临时 key 只有 1 小时；
- 直接把 OIDC token 当 `--api-key` 用会 403，必须先换 key；
- 用 `--skip-duplicate`，重复触发不会失败。

## 5. 发布流程

1. 更新 `CHANGELOG.md`；
2. 修改 `release.env` 的 `PACKAGE_VERSION` 和 `PACKAGE_RELEASE_NOTES`；
3. 本地打包验证（产物在 `nupkgs/`，已 gitignore，不推送）：

```bash
source ./release.env
dotnet pack src/NHibernate.AspNetCore.Identity -c Release --output ./nupkgs/
```

4. 提交后打标签并推送，标签触发工作流：

```bash
git tag 10.0.5
git push origin master 10.0.5
```

5. 在 Actions 里确认 run 成功，再到 nuget.org 确认包已出现（验证和索引最长约 1 小时）。

## 6. 本仓库当前配置

| 项 | 值 |
| --- | --- |
| 策略名称 | `nhibernate-aspnetcore-identity` |
| Package owner | `beginor` |
| Repository | `nhibernate/NHibernate.AspNetCore.Identity` |
| Workflow File | `publish-nuget.yml` |
| Glob | `NHibernate.AspNetCore.Identity` |
| GitHub 变量 | `NUGET_USER=beginor` |

## 常见问题

| 现象 | 原因 |
| --- | --- |
| `No matching trust policy owned by user '...'` | `NUGET_USER` 不是策略所属账号，或策略的 Workflow/Environment 与工作流不一致 |
| 403 `The specified API key has expired` | 本地 `dotnet nuget push` 只能走 API key，CI 才支持 Trusted Publishing |
| 包版本变成 `1.0.0` | `release.env` 里的变量没有 `export` |
| 策略显示 Pending | 私有仓库的策略初始只有 7 天有效期，首次成功换取 key 后永久激活 |
