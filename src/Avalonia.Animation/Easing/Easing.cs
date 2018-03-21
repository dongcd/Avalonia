﻿using Avalonia.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.ComponentModel;

namespace Avalonia.Animation
{
    /// <summary>
    /// Base class for all Easing classes.
    /// </summary>
    public abstract class Easing : IEasing
    {
        /// <inheritdoc/>
        public abstract double Ease(double progress);

        static Dictionary<string, Type> _easingTypes;

        static readonly Type s_thisType = typeof(Easing);

        /// <summary>
        /// Parses a Easing type string.
        /// </summary>
        /// <param name="e">The Easing type string.</param>
        /// <returns>Returns the instance of the parsed type.</returns>
        public static Easing Parse(string e)
        {
            // TODO: There should be a better way to
            //       find all the subclasses than this method...
            if (_easingTypes == null)
            {
                _easingTypes = new Dictionary<string, Type>();

                var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                                      .SelectMany(p => p.GetTypes())
                                      .Where(p => p.Namespace == s_thisType.Namespace)
                                      .Where(p => p.IsSubclassOf(s_thisType))
                                      .Select(p=>p).ToList();

                foreach (var easingType in derivedTypes)
                    _easingTypes.Add(easingType.Name, easingType);
            }

            if (_easingTypes.ContainsKey(e))
            {
                var type = _easingTypes[e];
                return (Easing)Activator.CreateInstance(type);
            }
            else
            {
                throw new FormatException($"Easing \"{e}\" was not found in {s_thisType.Namespace} namespace.");
            }
        }

    }
}
