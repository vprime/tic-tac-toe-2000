using System.Collections.Generic;
using UnityEngine;

namespace Environment.Components
{
    public static class DigitCharacterMap
    {
        public static readonly Dictionary<char, List<DigitSegments>> Map = new()
        {
            {'A', new() { DigitSegments.TopLeftVert, DigitSegments.TopLeftHoriz, DigitSegments.TopRightHoriz, DigitSegments.TopRightHoriz, DigitSegments.CenterLeftHoriz, DigitSegments.CenterRightHoriz,DigitSegments.BottomLeftVert, DigitSegments.BottomRightHoriz}},
            {'O', new() { DigitSegments.TopLeftVert, DigitSegments.TopLeftHoriz, DigitSegments.TopRightHoriz, DigitSegments.TopRightVert, DigitSegments.BottomLeftVert, DigitSegments.BottomLeftHoriz, DigitSegments.BottomRightHoriz, DigitSegments.BottomRightVert}},
            {'X', new() { DigitSegments.TopLeftDiag, DigitSegments.TopRightDiag,DigitSegments.BottomLeftDiag, DigitSegments.BottomRightDiag}},
        };
    }
}
