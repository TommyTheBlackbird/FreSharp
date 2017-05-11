using System.Collections;
using FREObject = System.IntPtr;
namespace TuaRua.FreSharp {
    /// <summary>
    /// FreObjectSharp wraps a C FREObject with helper methods.
    /// </summary>
    public class FreObjectSharp {
        /// <summary>
        /// Returns the associated C FREObject of the C# FREObject.
        /// </summary>
        /// <returns></returns>
        public FREObject RawValue { get; set; } = FREObject.Zero;

        /// <summary>
        /// Returns the C# FREObject as an object.
        /// </summary>
        public object Value => FreSharpHelper.GetAsObject(RawValue);

        /// <summary>
        /// Creates an empty C# FREObject
        /// </summary>
        public FreObjectSharp() { }

        /// <summary>
        /// Creates a C# FREObject from a C FREObject
        /// </summary>
        /// <param name="freObject"></param>
        public FreObjectSharp(FREObject freObject) {
            RawValue = freObject;
        }
        /// <summary>
        /// Creates a C# FREObject from a string
        /// </summary>
        /// <param name="value"></param>
        public FreObjectSharp(string value) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(value, ref resultPtr);
        }
        /// <summary>
        /// Creates a C# FREObject from a bool
        /// </summary>
        /// <param name="value"></param>
        public FreObjectSharp(bool value) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(value, ref resultPtr);
        }
        /// <summary>
        /// Creates a C# FREObject from a double
        /// </summary>
        /// <param name="value"></param>
        public FreObjectSharp(double value) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(value, ref resultPtr);
        }
        /// <summary>
        /// Creates a C# FREObject from an int
        /// </summary>
        /// <param name="value"></param>
        public FreObjectSharp(int value) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(value, ref resultPtr);
        }
        /// <summary>
        /// Creates a C# FREObject from a uint
        /// </summary>
        /// <param name="value"></param>
        public FreObjectSharp(uint value) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(value, ref resultPtr);
        }

        /// <summary>
        /// Creates a C# FREObject with given class name
        /// </summary>
        /// <param name="className"></param>
        /// <param name="args"></param>
        public FreObjectSharp(string className, ArrayList args) {
            uint resultPtr = 0;
            RawValue = FreSharpHelper.Core.getFREObject(className, FreSharpHelper.ArgsToArgv(args),
                FreSharpHelper.GetArgsC(args), ref resultPtr);
            var status = (FreResultSharp)resultPtr;
            if (status == FreResultSharp.Ok) {
                return;
            }
            FreSharpHelper.ThrowFreException(status, "cannot create class " + className, this);

        }

        /// <summary>
        /// Calls a method on a C# FREObject.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public FreObjectSharp CallMethod(string methodName, ArrayList args) {
            uint resultPtr = 0;
            var ret = new FreObjectSharp(FreSharpHelper.Core.callMethod(RawValue, methodName,
                FreSharpHelper.ArgsToArgv(args), FreSharpHelper.GetArgsC(args), ref resultPtr));
            var status = (FreResultSharp)resultPtr;

            if (status == FreResultSharp.Ok) {
                return ret;
            }

            FreSharpHelper.ThrowFreException(status, "cannot call method " + methodName, ret);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <param name="returnArray"></param>
        /// <returns></returns>
        public FreArraySharp CallMethod(string methodName, ArrayList args, bool returnArray) {
            uint resultPtr = 0;
            var ret = new FreArraySharp(FreSharpHelper.Core.callMethod(RawValue, methodName,
                FreSharpHelper.ArgsToArgv(args), FreSharpHelper.GetArgsC(args), ref resultPtr));
            var status = (FreResultSharp)resultPtr;
            if (status == FreResultSharp.Ok) {
                return ret;
            }

            FreSharpHelper.ThrowFreException(status, "cannot call method " + methodName, ret);
            return null;
        }

        /// <summary>
        /// Returns the property of the C# FREObject of the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FreObjectSharp GetProperty(string name) {
            return FreSharpHelper.GetProperty(RawValue, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetProperty(string name, object value)
        {
            FreSharpHelper.SetProperty(RawValue, name, value);
        }

        
        /// <summary>
        /// Returns the Actionscript type of the C# FREObject. !Important - your ane must include ANEUtils.as in com.tuarua
        /// </summary>
        /// <returns></returns>
        public new FreObjectTypeSharp GetType() {
            return FreSharpHelper.GetType(RawValue);
        }

    }
}