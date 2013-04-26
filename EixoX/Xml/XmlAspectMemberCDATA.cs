﻿using EixoX.Text.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EixoX.Xml
{
    public class XmlAspectMemberCDATA
        : XmlAspectMember
    {
        private TextAdapter _Adapter;

        public XmlAspectMemberCDATA(ClassAcessor acessor, string localName, bool mandatory, TextAdapter adapter)
            : base(acessor, localName, mandatory)
        {
            this._Adapter = adapter;
        }

        protected override void WriteXml(object entity, XmlElement parent, string localName, bool mandatory)
        {
            object value = GetValue(entity);
            if (_Adapter.IsEmpty(value))
            {
                if (mandatory)
                {
                    XmlElement member = parent.OwnerDocument.CreateElement(localName);
                    parent.AppendChild(member);
                }
            }
            else
            {
                XmlElement member = parent.OwnerDocument.CreateElement(localName);
                parent.AppendChild(member);

                string content = _Adapter.FormatObject(value);

                member.AppendChild(parent.OwnerDocument.CreateCDataSection(content));
            }

        }

        protected override void ReadXml(object entity, XmlElement parent, string localName, bool mandatory)
        {
            XmlElement element = parent[localName];
            if (element != null)
            {
                string content = element.InnerText;
                object value = _Adapter.ParseObject(content);
                SetValue(entity, value);
            }
        }
    }
}
