//Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//Microsoft Open Technologies would like to thank its contributors, a list
//of whom are at http://aspnetwebstack.codeplex.com/wikipage?title=Contributors.

//Licensed under the Apache License, Version 2.0 (the "License"); you
//may not use this file except in compliance with the License. You may
//obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//implied. See the License for the specific language governing permissions
//and limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

// namespace modified to prevent naming collisions
namespace MessageHandler.EventProcessing.Runtime.Serialization
{
    /// <summary>
    /// Helper to evaluate different method on dynamic objects
    /// </summary>
    internal static class DynamicHelper
    {
        // Dev10 Bug 914027 - Changed the first parameter from dynamic to object, see comment at top for details
        public static object GetMemberValue(object obj, string memberName)
        {
            var callSite = GetMemberAccessCallSite(memberName);
            return callSite.Target(callSite, obj);
        }

        // dynamic d = new object();
        // object s = d.Name;
        // The following code gets generated for this expression:
        // callSite = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Name", typeof(Program), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
        // callSite.Target(callSite, d);
        // typeof(Program) is the containing type of the dynamic operation.
        // Dev10 Bug 914027 - Changed the callsite's target parameter from dynamic to object, see comment at top for details
        public static CallSite<Func<CallSite, object, object>> GetMemberAccessCallSite(string memberName)
        {
            var binder = Binder.GetMember(CSharpBinderFlags.None, memberName, typeof(DynamicHelper), new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
            return GetMemberAccessCallSite(binder);
        }

        // Dev10 Bug 914027 - Changed the callsite's target parameter from dynamic to object, see comment at top for details
        public static CallSite<Func<CallSite, object, object>> GetMemberAccessCallSite(CallSiteBinder binder)
        {
            return CallSite<Func<CallSite, object, object>>.Create(binder);
        }

        // Dev10 Bug 914027 - Changed the first parameter from dynamic to object, see comment at top for details
        public static IEnumerable<string> GetMemberNames(object obj)
        {
            var provider = obj as IDynamicMetaObjectProvider;
            Debug.Assert(provider != null, "obj doesn't implement IDynamicMetaObjectProvider");

            Expression parameter = Expression.Parameter(typeof(object));
            return provider.GetMetaObject(parameter).GetDynamicMemberNames();
        }
    }
}
