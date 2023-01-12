using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Matrix2x2;

/// The HexUtility library is used to convert between Unity's square grid and a hexagonal
/// world subdivision.
public static class HexUtils
{

    // Basis vectors for converting (X, Y) coordinates into axial (Q, R, <S>) coordinates.
    // This creates the necessary skew for converting squares to the (superior) hexagonal
    // layout
    private static Matrix2x2 HEX_TO_WORLD_BASIS = new Matrix2x2(
        Mathf.Sqrt(3)/2, Mathf.Sqrt(3)/4,
        0, 3.0f/4
    );

    /// Uses the hex-to-world basis transformation to convert hex coordinates to world
    /// coordinates
    public static Vector2 HexToWorldCoordinates(int hexQ, int hexR, float scale = 1.0f) {
        Vector2 hexCoordinates = new Vector2(hexQ, hexR);

        return HexUtils.HexToWorldCoordinates(hexCoordinates, scale);
    }

    // Overwrite for HexToWorldCoordinates for use with Vector2 rather than parameter coords
    public static Vector2 HexToWorldCoordinates(Vector2 hexCoordinates, float scale = 1.0f) {

        // TODO: I'm pretty sure a matrix multiplication would be faster
        Vector2 worldCoordinates = HEX_TO_WORLD_BASIS * hexCoordinates;
        worldCoordinates *= scale;

        return worldCoordinates;
    }

}
