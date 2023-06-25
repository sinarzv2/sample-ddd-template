using System.Text.Json.Serialization;
using Common.Resources.Messages;
using Common.Utilities;

namespace Common.Models
{
    public class FluentResult
    {
        private readonly List<string> _successes;
        private readonly List<string> _errors;
        public FluentResult()
        {
            _successes = new List<string>();
            _errors = new List<string>();
            IsSuccess = true;
        }
        public bool IsSuccess { get; private set; }
        public IReadOnlyList<string> Successes => _successes;
        public IReadOnlyList<string> Errors => _errors;

        public void AddError(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            if (_errors.Contains(message))
                return;
            _errors.Add(message);
            IsSuccess = false;
        }
        public void AddErrors(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                AddError(message);
            }
        }

        public void RemoveError(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            _errors.Remove(message);
            if (!_errors.Any())
                IsSuccess = true;
        }

        public void CleareErrorMessages()
        {
            _errors.Clear();
            IsSuccess = true;
        }
        public void AddSuccess(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            if (_successes.Contains(message))
                return;
            _successes.Add(message);
        }
        public void AddSuccesses(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _successes.Add(message);
            }
        }
        public void RemoveSuccess(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            _successes.Remove(message);
        }

        public void CleareSuccessMessages()
        {
            _successes.Clear();
        }
    }

    public class FluentResult<TData> : FluentResult
        where TData : class
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData Data { get; private set; }


        public FluentResult<TData> Success(TData data, string message)
        {
            Data = data;
            AddSuccess(message);
            return this;
        }

        public void SetData(TData data)
        {
            Data = data;
        }
    }


}
