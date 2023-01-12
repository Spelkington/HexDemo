using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexGrid;
using static HexUtils;

/// The in-game singleton object responsible for procedurally generating the world terrain.
///
/// Responsible for initiating the grid generation and providing the HexGrid a delegate
/// method for how hexes should be placed within the world.
public class ProcGen : MonoBehaviour {
    /// Radius of the map in hex count
    [SerializeField] int radius;

    /// GameObject references for grass, coast, and water tile prefabs
    [SerializeField] GameObject grass;
    [SerializeField] GameObject coast;
    [SerializeField] GameObject water;

    /// Radii of the generated island and coast
    [SerializeField] float islandRadius;
    [SerializeField] float coastWidth;

    // Start is called before the first frame update
    void Start() {
        BeginGeneration();
    }

    /// Instantiate the hex grid and provide it the HexEntry delegate
    void BeginGeneration() {
        HexGrid grid = new HexGrid(radius);
        grid.Generate(this.OnNewHexEntry, 1.05f);
    }

    void OnNewHexEntry(Vector2 hexCoordinates) {
        // Convert hex to world coordinates
        //
        // TODO: Find some cleaner way to fetch this from the Grid itself rather than
        // re-calculating each time
        Vector2 worldCoordinates = HexUtils.HexToWorldCoordinates(hexCoordinates, 1.05f);

        GameObject targetTile = this.DetermineTerrain(hexCoordinates);

        Instantiate(targetTile, worldCoordinates, Quaternion.identity);
    }

    /// Determines, given the hex coordinates and island radius, whether a given hex
    /// should be land, coast, or water.
    GameObject DetermineTerrain(Vector2 hexCoordinates) {
        // Get the magnitude of the hex coordinates
        float distanceFromCenter = hexCoordinates.magnitude;

        // Depending on which band away from the radius the hex is, determine if it is
        // land, coast, or water
        if (distanceFromCenter >= this.islandRadius) {
            return this.water;
        }
        else if (distanceFromCenter >= this.islandRadius - coastWidth) {
            return this.coast;
        }
        else {
            return this.grass;
        }
    }

}
