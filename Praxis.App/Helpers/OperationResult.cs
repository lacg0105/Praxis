using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Praxis.Model.Emun;

namespace Praxis.App
{
    public class OperationResult
    {
        public EnumOperationResult IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        private string _type = "operation";
        public string Type { get { return _type; } }
        public bool IsModal { get; set; }

        public OperationResult(object data)
        {
            IsSuccess = EnumOperationResult.Success;
            Data = data;
        }
        public OperationResult(EnumOperationResult isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public OperationResult(EnumOperationResult isSuccess, string message, object data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public OperationResult(EnumOperationResult isSuccess, string message, object data, bool Modal)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            IsModal = Modal;
        }

        public static OperationResult Success()
        {
            return new OperationResult(EnumOperationResult.Success, "La operacion se realizó con éxito");
        }

        public static OperationResult Success(string Message)
        {
            return new OperationResult(EnumOperationResult.Success, Message);
        }
        public static OperationResult Failure(string Message)
        {
            return new OperationResult(EnumOperationResult.Failure, Message);
        }
        public static OperationResult Success(string Message, object Data)
        {
            return new OperationResult(EnumOperationResult.Success, Message, Data);
        }
        public static OperationResult Failure(string Message, object Data)
        {
            return new OperationResult(EnumOperationResult.Failure, Message, Data);
        }

        public static OperationResult Success(object Data)
        {
            return new OperationResult(EnumOperationResult.Success, "La operacion se realizó con éxito", Data);
        }

        public static OperationResult Failure(string Message, object Data, bool IsModal)
        {
            return new OperationResult(EnumOperationResult.Failure, Message, Data, IsModal);
        }

        public static OperationResult Warning(string Message)
        {
            return new OperationResult(EnumOperationResult.Warning, Message);
        }
        public static OperationResult Warning(string Message, object Data)
        {
            return new OperationResult(EnumOperationResult.Warning, Message, Data);
        }

    }
}