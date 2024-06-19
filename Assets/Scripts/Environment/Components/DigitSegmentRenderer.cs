using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment.Components
{
    public class DigitSegmentRenderer : MonoBehaviour
    {
        [SerializeField] private Material darkSegment;
        [SerializeField] private Material litSegment;
        
        [SerializeField] private Renderer topLeftVert;
        [SerializeField] private Renderer topLeftDiag;
        [SerializeField] private Renderer topLeftHoriz;
        [SerializeField] private Renderer topCenterVert;
        [SerializeField] private Renderer topRightHoriz;
        [SerializeField] private Renderer topRightDiag;
        [SerializeField] private Renderer topRightVert;
        [SerializeField] private Renderer centerLeftHoriz;
        [SerializeField] private Renderer centerRightHoriz;
        [SerializeField] private Renderer bottomLeftVert;
        [SerializeField] private Renderer bottomLeftDiag;
        [SerializeField] private Renderer bottomLeftHoriz;
        [SerializeField] private Renderer bottomCenterVert;
        [SerializeField] private Renderer bottomRightHoriz;
        [SerializeField] private Renderer bottomRightDiag;
        [SerializeField] private Renderer bottomRightVert;

        public void SetCharacter(char character)
        {
            if (DigitCharacterMap.Map.TryGetValue(character, out var value))
            {
                SetSegments(value);
            }
            else
            {
                Debug.LogError($"Character '{character}' has no segment map in DigitalCharacterMap");
            }
        }

        public void SetSegments(List<DigitSegments> segmentsList)
        {
            foreach (var segmentOption in Enum.GetValues(typeof(DigitSegments)).Cast<DigitSegments>())
            {
                var segmentRenderer = MapRendererToSegment(segmentOption);
                segmentRenderer.material = segmentsList.Contains(segmentOption) ? litSegment : darkSegment;
            }
        }

        public void LightAll()
        {
            foreach (var segmentOption in Enum.GetValues(typeof(DigitSegments)).Cast<DigitSegments>())
            {
                var segmentRenderer = MapRendererToSegment(segmentOption);
                segmentRenderer.material = litSegment;
            }
        }

        public void DarkenAll()
        {
            foreach (var segmentOption in Enum.GetValues(typeof(DigitSegments)).Cast<DigitSegments>())
            {
                var segmentRenderer = MapRendererToSegment(segmentOption);
                segmentRenderer.material = darkSegment;
            }
        }

        public void SetLitMaterial(Material newMaterial)
        {
            litSegment = newMaterial;
        }

        private Renderer MapRendererToSegment(DigitSegments segments)
        {
            return segments switch
            {
                DigitSegments.TopLeftVert => topLeftVert,
                DigitSegments.TopLeftDiag => topLeftDiag,
                DigitSegments.TopLeftHoriz => topLeftHoriz,
                DigitSegments.TopCenterVert => topCenterVert,
                DigitSegments.TopRightHoriz => topRightHoriz,
                DigitSegments.TopRightDiag => topRightDiag,
                DigitSegments.TopRightVert => topRightVert,
                DigitSegments.CenterLeftHoriz => centerLeftHoriz,
                DigitSegments.CenterRightHoriz => centerRightHoriz,
                DigitSegments.BottomLeftVert => bottomLeftVert,
                DigitSegments.BottomLeftDiag => bottomLeftDiag,
                DigitSegments.BottomLeftHoriz => bottomLeftHoriz,
                DigitSegments.BottomCenterVert => bottomCenterVert,
                DigitSegments.BottomRightHoriz => bottomRightHoriz,
                DigitSegments.BottomRightDiag => bottomRightDiag,
                DigitSegments.BottomRightVert => bottomRightVert,
                _ => throw new ArgumentOutOfRangeException(nameof(segments), segments, null)
            };
        }
    }
}
