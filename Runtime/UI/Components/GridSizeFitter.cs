using UnityEngine;
using UnityEngine.UI;

namespace MewtonGames.UI.Components
{
    [RequireComponent(typeof(RectTransform))] [RequireComponent(typeof(GridLayoutGroup))] [AddComponentMenu("MewtonGames/UI/Components/GridSizeFitter")]
    public class GridSizeFitter : MonoBehaviour
    {
        [SerializeField] private bool _fitOnAwake;
        [SerializeField] private bool _forcedSquare;

        [Space] 
        [SerializeField] private bool _fitHorizontal;
        [SerializeField] [Min(1)] private int _horizontalCellsCount = 1;

        [Space]
        [SerializeField] private bool _fitVertical;
        [SerializeField] [Min(1)] private int _verticalCellsCount = 1;

        public GridSizeFitter SetForcedSquare(bool value)
        {
            _forcedSquare = value;
            return this;
        }

        public GridSizeFitter SetFitHorizontal(bool value)
        {
            _fitHorizontal = value;
            return this;
        }

        public GridSizeFitter SetHorizontalCellsCount(int value)
        {
            if (value <= 0)
            {
                value = 1;
            }

            _horizontalCellsCount = value;
            return this;
        }

        public GridSizeFitter SetFitVertical(bool value)
        {
            _fitVertical = value;
            return this;
        }

        public GridSizeFitter SetVerticalCellsCount(int value)
        {
            if (value <= 0)
            {
                value = 1;
            }

            _verticalCellsCount = value;
            return this;
        }

        public void Fit()
        {
            var rectTransform = GetComponent<RectTransform>();
            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var rect = rectTransform.rect;
            var spacing = gridLayoutGroup.spacing;
            var padding = gridLayoutGroup.padding;
            var cellSize = gridLayoutGroup.cellSize;
            var ratio = cellSize.x / cellSize.y;

            if (_fitHorizontal)
            {
                cellSize.x = (rect.width - padding.horizontal - spacing.x * (_horizontalCellsCount - 1)) / _horizontalCellsCount;
                cellSize.y = cellSize.x / ratio;
            }

            if (_fitVertical)
            {
                cellSize.y = (rect.height - padding.vertical - spacing.y * (_verticalCellsCount - 1)) / _verticalCellsCount;
                cellSize.x = cellSize.y * ratio;
            }

            if (_forcedSquare)
            {
                if (_fitHorizontal)
                {
                    cellSize.y = cellSize.x;
                }
                else if (_fitVertical)
                {
                    cellSize.x = cellSize.y;
                }
                else
                {
                    if (cellSize.x > cellSize.y)
                    {
                        cellSize.y = cellSize.x;
                    }
                    else
                    {
                        cellSize.x = cellSize.y;
                    }
                }
            }

            gridLayoutGroup.cellSize = cellSize;
        }

        private void Awake()
        {
            if (_fitOnAwake)
            {
                Fit();
            }
        }
    }
}