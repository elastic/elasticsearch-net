using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
  public enum DateRounding
  {
  /// <summary>
    /// (the default), rounds to the lowest whole unit of this field.
  /// </summary>
    Floor,
    /// <summary>
    /// Rounds to the highest whole unit of this field.
    /// </summary>
    Ceiling,
    /// <summary>
    ///  Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor or is exactly halfway, this function behaves like floor. If the millisecond value is closer to the ceiling, this function behaves like ceiling.
    /// </summary>
    Half_Floor,
    /// <summary>
    /// Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor, this function behaves like floor. If the millisecond value is closer to the ceiling or is exactly halfway, this function behaves like ceiling.
    /// </summary>
    Half_Ceiling,
    /// <summary>
    /// Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor, this function behaves like floor. If the millisecond value is closer to the ceiling, this function behaves like ceiling. If the millisecond value is exactly halfway between the floor and ceiling, the ceiling is chosen over the floor only if it makes this field’s value even.
    /// </summary>
    Half_Even
  }
}
