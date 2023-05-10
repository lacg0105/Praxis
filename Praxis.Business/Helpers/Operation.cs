using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Model.Emun;

namespace Praxis.Business.Helpers
{
    public class Operation
    {
        public EnumOperationResult IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<object> lstData { get; set; }
        public bool IsModal { get; set; }

        public Operation(EnumOperationResult isSuccess, string message, bool modal)
        {
            IsSuccess = isSuccess;
            Message = message;
            IsModal = modal;
        }

        public Operation(EnumOperationResult isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public Operation(EnumOperationResult isSuccess, string message, object data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public Operation(EnumOperationResult isSuccess, string message, List<object> data)
        {
            IsSuccess = isSuccess;
            Message = message;
            lstData = data;

        }

        public Operation(EnumOperationResult isSuccess, string message, List<object> data, bool _IsModal)
        {
            IsSuccess = isSuccess;
            Message = message;
            lstData = data;
            IsModal = _IsModal;
        }
        public Operation(EnumOperationResult isSuccess, string message, object data, bool _IsModal)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            IsModal = _IsModal;
        }



        public static Operation Success()
        {
            return new Operation(EnumOperationResult.Success, "La operacion se realizó con éxito");
        }

        public static Operation Success(string Message)
        {
            return new Operation(EnumOperationResult.Success, Message);
        }
        public static Operation Failure(string Message)
        {
            return new Operation(EnumOperationResult.Failure, Message);
        }
        public static Operation Success(string Message, object Data)
        {
            return new Operation(EnumOperationResult.Success, Message, Data);
        }

        public static Operation Success(string Message, List<object> Data)
        {
            return new Operation(EnumOperationResult.Success, Message, Data);
        }

        public static Operation Failure(string Message, List<object> Data)
        {
            return new Operation(EnumOperationResult.Failure, Message, Data, true);
        }

        public static Operation Failure(string Message, object Data)
        {
            return new Operation(EnumOperationResult.Failure, Message, Data);
        }

        public static Operation Warning(string Message)
        {
            return new Operation(EnumOperationResult.Warning, Message);
        }
        public static Operation Warning(string Message, object Data)
        {
            return new Operation(EnumOperationResult.Warning, Message, Data);
        }
        public static Operation Warning(string Message, object Data, bool _IsModal)
        {
            return new Operation(EnumOperationResult.Warning, Message, Data, _IsModal);
        }


        public static Operation Failure(string Message, bool modal)
        {
            return new Operation(EnumOperationResult.Failure, Message, modal);
        }

        public static Operation Failure(string Message, object Data, bool modal)
        {
            return new Operation(EnumOperationResult.Failure, Message, Data, modal);
        }
    }
}
