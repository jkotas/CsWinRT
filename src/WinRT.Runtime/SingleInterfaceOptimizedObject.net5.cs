﻿using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace WinRT
{
    public class SingleInterfaceOptimizedObject : IWinRTObject, IDynamicInterfaceCastable
    {
        private Type _type;
        private IObjectReference _obj;

        public SingleInterfaceOptimizedObject(Type type, IObjectReference objRef)
        {
            _type = type;
            Type helperType = type.FindHelperType();
            var vftblType = helperType.FindVftblType();
            if (vftblType is null)
            {
                _obj = objRef.As<IUnknownVftbl>(GuidGenerator.GetIID(helperType));
            }
            else
            {
                _obj = (IObjectReference)typeof(IObjectReference).GetMethod("As", Type.EmptyTypes).MakeGenericMethod(vftblType).Invoke(objRef, null);
            }
        }

        IObjectReference IWinRTObject.NativeObject => _obj;
        bool IWinRTObject.HasUnwrappableNativeObject => false;

        private Lazy<ConcurrentDictionary<RuntimeTypeHandle, IObjectReference>> _lazyQueryInterfaceCache = new();
        ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> IWinRTObject.QueryInterfaceCache => _lazyQueryInterfaceCache.Value;
        private Lazy<ConcurrentDictionary<RuntimeTypeHandle, object>> _lazyAdditionalTypeData = new();
        ConcurrentDictionary<RuntimeTypeHandle, object> IWinRTObject.AdditionalTypeData => _lazyAdditionalTypeData.Value;

        bool IDynamicInterfaceCastable.IsInterfaceImplemented(RuntimeTypeHandle interfaceType, bool throwIfNotImplemented)
        {
            if (_type.Equals(Type.GetTypeFromHandle(interfaceType)))
            {
                return true;
            }
            return (this as IWinRTObject).IsInterfaceImplementedFallback(interfaceType, throwIfNotImplemented);
        }

        IObjectReference IWinRTObject.GetObjectReferenceForType(RuntimeTypeHandle interfaceType)
        {
            if (_type.Equals(Type.GetTypeFromHandle(interfaceType)))
            {
                return _obj;
            }
            return (this as IWinRTObject).GetObjectReferenceForTypeFallback(interfaceType);
        }

    }
}
