using System.Collections.Generic;
using Tom.Api;
using Tom.Api.Request;
using Tom.ChineseChess.Sdk.Response;

namespace Tom.ChineseChess.Sdk.Request
{
    public class SquareReadyRequest : IRequest<SquareReadyResponse>
    {
        #region IRequest Members
        private string apiVersion = "1.0";
		private bool  needEncrypt=false;
        private IObject bizModel;
        public string BizContent { get; set; }
        public bool Debug { get; set; }
        public string Session { get; set; }

        public string GetApiName()
        {
            return "tom.chinesechess.chessing.ready";
        }

        public string GetApiVersion()
        {
            return apiVersion;
        }
        public void SetApiVersion(string apiVersion)
        {
            this.apiVersion = apiVersion;
        }

        public bool GetNeedEncrypt()
        {
            return needEncrypt;
        }

        public void SetNeedEncrypt(bool needEncrypt)
        {
            this.needEncrypt = needEncrypt;
        }

        public IObject GetBizModel()
        {
            return this.bizModel;
        }


        public void SetBizModel(IObject bizModel)
        {
            this.bizModel = bizModel;
        }

        public IDictionary<string, string> GetParameters()
        {
            ParamDictionary parameters = new ParamDictionary();
            parameters.Add("biz_content", this.BizContent);
            return parameters;
        }

        #endregion

    }
}
