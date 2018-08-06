using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project20180804.Core.Framework;
using Qiniu.IO.Model;
using Qiniu.Util;
using System.IO;

namespace Project20180804.Core.Web.Controllers
{
    public class QiNiuController : Controller
    {
        private readonly IOptions<AppSettings> _appSetting;

        public QiNiuController(IOptions<AppSettings> appSetting)
        {
            _appSetting = appSetting;
        }

        [HttpGet]
        public IActionResult GetToken(string fileName)
        {
            Mac mac = new Mac(_appSetting.Value.QiNiuSetting.AccessKey, _appSetting.Value.QiNiuSetting.SecretKey);
            var ex = Path.GetExtension(fileName);
            var newFileName = $"{Path.GetRandomFileName()}{ex}";
            PutPolicy putPolicy = new PutPolicy
            {
                //存储名称
                Scope = _appSetting.Value.QiNiuSetting.Bucket
            };
            putPolicy.SetExpires(3600);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return Ok(new
            {
                Token = token,
                FileName = newFileName
            });
        }

        public IActionResult GetToken()
        {
            Mac mac = new Mac(_appSetting.Value.QiNiuSetting.AccessKey, _appSetting.Value.QiNiuSetting.SecretKey);
            PutPolicy putPolicy = new PutPolicy
            {
                //存储名称
                Scope = _appSetting.Value.QiNiuSetting.Bucket
            };
            putPolicy.SetExpires(3600);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return Ok(token);
        }
    }
}