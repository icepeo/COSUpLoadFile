using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using COSUpLoadFile.CosApi.API;

namespace COSUpLoadFile.Common
{
    public class Function
    {
        /// <summary>
        /// 读取远程目录列表数据
        /// </summary>
        /// <param name="bn">bucketName</param>
        /// <param name="ml">远程路径</param>
        /// <param name="num">读取目录项数量</param>
        /// <param name="ct"></param>
        /// <param name="o"></param>
        /// <param name="fp"></param>
        /// <param name="prefix">前缀</param>
        /// <param name="mes">输出出错信息</param>
        /// <returns></returns>
        private List<FolderProperty> GetFolderData(string bn, string ml, int num, string ct, int o, FolderPattern fp, string prefix = "",out string mes)
        {
            CosCloud cos = new CosCloud(APP_ID, SECRET_ID, SECRET_KEY);
            string result = "";
            result = cos.GetFolderList(bn, ml, num, ct, o, fp);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            BaseFolderModel bfm = jss.Deserialize<BaseFolderModel>(result);
            if (bfm.code != 0 && bfm.message != "SUCCESS")
            {
                MessageBox.Show("获取目录出错!错误码：" + bfm.code + ";错误信息：" + bfm.message + ";");
                return null;
            }
            return bfm.data.infos;
        }
    }
}
