// using UnityEngine;
// using UnityEngine.UI;

// public class LevelGenerator : MonoBehaviour
// {
//     public Texture2D[] maps;

//     public Texture2D map;

//     public ColorToPrefab[] colorMappings;

//     int level = LevelSelector.selectedLevel;

//     void Awake()
//     {
//         map = maps[level - 1];
//         GenerateLevel();
//     }

//     void GenerateLevel() {
//         for (int x = 0; x < map.width; x++) {
//             for  (int y = 0; y < map.height; y++) {
//                 GenerateTile(x, y);
//             }
//         }
//     }

//     void GenerateTile (int x, int y) {
//         Color pixelColor = map.GetPixel(x, y);

//         if (pixelColor.a == 0) {
//             // The pixel is transparrent, let's ignore it
//             return;
//         }

//         foreach (ColorToPrefab colorMapping in colorMappings) {
//             if (colorMapping.color.Equals(pixelColor)) {
//                 Vector2 position = new Vector2(x, y - 27);
//                 Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
//             }
//         }
//     }

// }
