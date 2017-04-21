using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;

namespace MessageHandler.Runtime
{
    public class DynamicVariable : DynamicObject
    {
        private readonly IDictionary<string, Variable> _values;

        public DynamicVariable(string scope, IEnumerable<Variable> values)
        {
            _values = values
                .Where(p => p.ScopeType == scope)
                .ToDictionary(p => p.Name, p => p);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;
            if (binder.Type.IsInstanceOfType(_values))
            {
                result = _values;
            }
            else
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Unable To Convert Type", binder.Type));
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValue(binder.Name);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new NotImplementedException();
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var key = GetKey(indexes);
            result = null;
            if (!String.IsNullOrEmpty(key))
            {
                result = GetValue(key);
            }
            return true;
        }

        private static string GetKey(IList<object> indexes)
        {
            if (indexes.Count == 1)
            {
                return (string)indexes[0];
            }
            return null;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _values.Keys;
        }

        private object GetValue(string name)
        {
            Variable result;
            return _values.TryGetValue(name, out result) ? result.Value : null;
        }
    }
}