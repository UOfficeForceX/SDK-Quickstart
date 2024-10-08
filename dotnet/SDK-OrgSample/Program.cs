using Ede.Uofx.PubApi.Sdk.NetStd.Models.Base;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;

//設定金鑰
UofxService.Key = "xxx";

//設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw";

try
{
	//呼叫站台進行起單
	var allDepts = await UofxService.BASE.Department.Get(new DepartmentGetModel()
	{
		CorpCode = "love",
		IncludeSubDept = true
	});
	Console.WriteLine(UofxService.Json.Convert(allDepts));

	var dept = allDepts.First();

	var result = await UofxService.BASE.Department.Update(new DepartmentUpdateModel()
	{
		CorpCode = "love",
		OriginalCode = dept.Code,
		Code = dept.Code,
		Name = dept.Name,
		DeptLevelCode = dept.DeptLevelCode,
		Description = "Sample 測試"
	});

	Console.WriteLine(result);
}
catch (Exception e)
{
	//將 exception 轉換成較容易判斷的 model
	var model = UofxService.Error.ConvertToModel(e);
	//將 model 轉成 json 格式印出
	Console.WriteLine(UofxService.Json.Convert(model));
}



