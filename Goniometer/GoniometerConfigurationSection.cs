using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Goniometer
{
    public class GoniometerConfigurationSection : ConfigurationSection
    {
        public static GoniometerConfigurationSection GetConfigurationSection()
        {
            return (GoniometerConfigurationSection)ConfigurationManager.GetSection("Goniometer_Controller");
        }
        
        [ConfigurationProperty("sensors", IsRequired=true, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SensorConfigurationCollection), AddItemName = "add")]
        public SensorConfigurationCollection Sensors
        {
            get { return (SensorConfigurationCollection)this["sensors"]; }
        }

        public class SensorConfigurationCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new SensorConfigurationElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                SensorConfigurationElement service = (SensorConfigurationElement)element;

                return getKey(service);
            }

            /// <summary>
            /// Gets or sets the named service element for the given index.
            /// </summary>
            /// <param name="index">The index of the named service element to get or set.</param>
            /// <returns>The named service element.</returns>
            public SensorConfigurationElement this[int index]
            {
                get
                {
                    return (SensorConfigurationElement)BaseGet(index);
                }
                set
                {
                    if (BaseGet(index) != null)
                    {
                        BaseRemove(index);
                    }
                    BaseAdd(index, value);
                }
            }

            /// <summary>
            /// Gets or sets the named service element for the given name.
            /// </summary>
            /// <param name="name">The name of the named service element to get or set.</param>
            /// <returns>The named service element.</returns>
            public new SensorConfigurationElement this[string name]
            {
                get
                {
                    return (SensorConfigurationElement)BaseGet(name);
                }
            }

            /// <summary>
            /// Gets the number of named service elements in this instance.
            /// </summary>
            public new int Count
            {
                get { return base.Count; }
            }

            public int IndexOf(SensorConfigurationElement service)
            {
                return BaseIndexOf(service);
            }

            public void RemoveAt(int index)
            {
                BaseRemoveAt(index);
            }

            public void Add(SensorConfigurationElement item)
            {
                BaseAdd(item);
            }

            public void Clear()
            {
                BaseClear();
            }

            public bool Contains(SensorConfigurationElement item)
            {
                return BaseIndexOf(item) >= 0;
            }

            public void CopyTo(SensorConfigurationElement[] array, int arrayIndex)
            {
                base.CopyTo(array, arrayIndex);
            }

            public new bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove(SensorConfigurationElement item)
            {
                if (BaseIndexOf(item) >= 0)
                {
                    BaseRemove(item);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Gets the key by which named service elements are mapped in the base class.
            /// </summary>
            /// <param name="service">The named service element to get the key from.</param>
            /// <returns>The key.</returns>
            private string getKey(SensorConfigurationElement service)
            {
                return service.Name;
            }
        }

        public class SensorConfigurationElement : ConfigurationElement
        {
            [ConfigurationProperty("name", IsRequired = true)]
            public string Name
            {
                get { return (string)this["name"]; }
                set { this["name"] = value; }
            }

            [ConfigurationProperty("type", IsRequired = true)]
            public string Type
            {
                get { return (string)this["type"]; }
                set { this["type"] = value; }
            }

            [ConfigurationProperty("port", IsRequired = true)]
            public string Port
            {
                get { return (string)this["port"]; }
                set { this["port"] = value; }
            }
        }
    }
}
