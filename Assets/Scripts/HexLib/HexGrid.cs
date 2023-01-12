using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexUtils;

public class HexGrid
{

    /// A delegate function provided to Generate, run each time a new hexagon is placed.
    public delegate void OnNewHexEntry(Vector2 hexCoordinates);

    /// Radius in tiles of the hex grid
    private int radius;

    /// Instantiates a hexgrid with the given radius
    public HexGrid(int radius) {
        this.radius = radius;
    }

    /// Iterates through and generates the hexagonal grid, calling the provided delegate
    /// each time a new hex location is determined.
    public void Generate(OnNewHexEntry newEntryFunc, float scale = 1.0f) {

        for (int q = -1*this.radius; q < this.radius; q++)
        {
            for (int r = -1*this.radius; r < this.radius; r++)
            {
                Vector2 hexCoordinates = new Vector2(q, r);
                Vector2 worldCoordinates = HexUtils.HexToWorldCoordinates(q, r, scale);
                newEntryFunc(hexCoordinates);
            }
        }
    }
}
