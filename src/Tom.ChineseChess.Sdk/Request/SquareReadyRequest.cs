using System.Collections.Generic;
using Tom.Api;
using Tom.Api.Request;
using Tom.ChineseChess.Sdk.Response;

namespace Tom.ChineseChess.Sdk.Request
{
    public class SquareReadyRequest : IRequest<SquareReadyResponse>
    {
        public SquareReadyRequest()
        {
            Version = "1.0";
        }
        #region IRequest Members
        private bool  needEncrypt=false;
        private IObject bizModel;
        public string Biz_Content { get; set; }
        public bool Debug { get; set; }
        public string Session { get; set; }

        public string GetApiName()
        {
            return "tom.chinesechess.chessing.ready";
        }

        public string Version { get; set; }

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
            parameters.Add("biz_content", this.Biz_Content);
            return parameters;
        }

        #endregion

    }
}
