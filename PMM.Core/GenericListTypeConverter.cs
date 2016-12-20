using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PMM.Core
{
    public class GenericListTypeConverter<T> : TypeConverter
    {
       protected readonly TypeConverter _typeConverter;

       public GenericListTypeConverter()
        {
            _typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (_typeConverter == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
        }
    }
}
