using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using System;

//設定金鑰
UofxService.Key = "xxx";

//設定 UOF X 站台網址
UofxService.UofxServerUrl = "https://myuofx.com.tw";

//上傳檔案
var fileView = await UofxService.File.FileUpload(@"D:\sample\sample.pdf");
//轉換成檔案物件
var fileItem = new Ede.Uofx.FormSchema.UofxFormSchema.FileItem()
{
	Id = fileView.Id,
	FileName = fileView.FileName
};

//建立 外部起單物件
var doc = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchema()
{
	//申請者帳號
	Account = "Justin",
	//申請者部門代號
	DeptCode = "RD",
	////要 CallBack 的 Url
	//CallBackUrl = "https://hr-system.com.tw/uofx-sdk/callback",
	////客製資訊: 填入起單時間
	//CustomData = $"{DateTimeOffset.Now}",
	//將檔案物件入附件欄位
	AttachFiles = new List<Ede.Uofx.FormSchema.UofxFormSchema.FileItem>()
	{
		fileItem
	}
};

//建立 表單欄位物件，填寫表單欄位資料
doc.Fields = new Ede.Uofx.FormSchema.UofxFormSchema.UofxFormSchemaFields()
{
	C002 = "測試",
};

try
{
	//呼叫站台進行起單
	var traceId = await UofxService.BPM.ApplyForm(doc);

	Console.WriteLine($"Trace Id: {traceId}");
}
catch (Exception e)
{
	//將 exception 轉換成較容易判斷的 model
	var model = UofxService.Error.ConvertToModel(e);
	//將 model 轉成 json 格式印出
	Console.WriteLine(UofxService.Json.Convert(model));
}



