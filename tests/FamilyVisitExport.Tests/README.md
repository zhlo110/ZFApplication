# 家访导出验证

该控制台测试直接编译生产数据分组和 Excel 导出文件，使用合成线索及行政区数据验证；无需 Windows、DevExpress 或真实考生数据库。

本人户籍所在省按本人户籍市州归表，同省父母在其他市州不增加该省记录；跨省父母及继父母按其实际户籍市州另列招飞对象，相同省、市州和对象去重。`StudentCount` 按政审编号及原始招飞对象姓名计算独立人数，`RowCount` 计算包含跨省、跨市记录的总行数。缺少本人有效户籍时保留已知父母地点并告警。

安装 .NET 8 SDK 后，在仓库根目录执行：

```sh
dotnet run --project tests/FamilyVisitExport.Tests/FamilyVisitExport.Tests.csproj
```

测试复用仓库中的 EPPlus 7.1 程序集。编译缓存和临时工作簿位于系统临时目录，测试完成后删除工作簿。完整 Windows 程序仍需在安装 .NET Framework 4.8、DevExpress 21.2 的 Windows 环境中验证按钮流程。
