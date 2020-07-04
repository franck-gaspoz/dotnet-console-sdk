﻿using System;
using System.Collections.Generic;

namespace DotNetConsoleAppToolkit.Lib.Data
{
    public sealed class DataObjectReadOnlyException : Exception
    {
        public DataObjectReadOnlyException(IDataObject dataObject) : base(
            $"DataObject name='{dataObject}' is read only"
            ) { }
    }

    public sealed class DataObject : IDataObject
    {
        public string Name { get; private set; }

        public DataObject Parent { get; private set; }

        private readonly Dictionary<string, IDataObject> _attributes
            = new Dictionary<string, IDataObject>();

        public bool IsReadOnly { get; private set; }

        public DataObject(string name, bool isReadOnly = false)
        {
            Name = name;
            IsReadOnly = isReadOnly;
        }

        public void Set(ArraySegment<string> path, object value)
        {
            if (IsReadOnly) throw new DataObjectReadOnlyException(this);
            if (path.Count == 0) return;
            var attrname = path[0];
            if (_attributes.TryGetValue(attrname, out var attr))
            {
                if (path.Count == 1)
                {
                    _attributes[attrname] = new DataValue(attrname, value);
                }
                else
                    attr.Set(path.Slice(1), value);
            }
            else
            {
                var node = new DataObject(attrname);
                _attributes[attrname] = node;
                node.Set(path.Slice(1), value);
            }
        }

        public void Unset(ArraySegment<string> path)
        {
            if (IsReadOnly) throw new DataObjectReadOnlyException(this);
            if (path.Count == 0) return;
            var attrname = path[0];
            if (_attributes.TryGetValue(attrname, out var attr))
            {
                if (path.Count == 1)
                {
                    _attributes.Remove(attrname);
                }
                else
                    attr.Unset(path.Slice(1));
            }
        }

        public object Get(ArraySegment<string> path)
        {
            if (path.Count == 0) return null;
            var attrname = path[0];
            if (_attributes.TryGetValue(attrname, out var attr))
            {
                if (path.Count == 1) return attr;
                return attr.Get(path.Slice(1));
            }
            else
                return null;
        }

        public bool Has(ArraySegment<string> path)
            => GetPathOwner(path) != null;

        public object GetPathOwner(ArraySegment<string> path)
        {
            if (path.Count == 0) return null;
            var attrname = path[0];
            if (_attributes.ContainsKey(attrname))
            {
                if (path.Count == 1) return this;
                return GetPathOwner(path.Slice(1));
            }
            else
                return null;
        }
    }

}
