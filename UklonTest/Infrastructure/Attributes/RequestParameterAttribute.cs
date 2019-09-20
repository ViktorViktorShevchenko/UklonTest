using System;

namespace UklonTest.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequestParameterAttribute : Attribute
    {
        public string ParameterName { get; set; }
        public RequestParameterAttribute(string parameterName)
        {
            this.ParameterName = parameterName;
        }
    }
}
