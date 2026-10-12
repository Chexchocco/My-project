using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AI.Editor
{
    /// <summary>
    /// Editor utility to automate the organization and renaming of AI ScriptableObject assets.
    /// Uses AssetDatabase methods to preserve .meta file references and GUID links.
    /// </summary>
    public static class AIAssetOrganizer
    {
        private const string MenuPath = "Tools/Organize AI Assets";

        // Required target directory structure
        private static readonly string[] RequiredFolders = new[]
        {
            "Assets/Data/AI/Selectors",
            "Assets/Data/AI/Conditions",
            "Assets/Data/AI/Actions"
        };

        // Migration definitions: (SourceFileName, NewFileName, TargetDirectory)
        private struct AssetMoveTask
        {
            public string SourceFileName;
            public string NewFileName;
            public string TargetDirectory;

            public AssetMoveTask(string sourceFileName, string newFileName, string targetDirectory)
            {
                SourceFileName = sourceFileName;
                NewFileName = newFileName;
                TargetDirectory = targetDirectory;
            }
        }

        private static readonly AssetMoveTask[] Tasks = new[]
        {
            new AssetMoveTask("SelfSelector.asset", "Sel_Self.asset", "Assets/Data/AI/Selectors"),
            new AssetMoveTask("EnemyTargetFinder.asset", "Sel_ClosestEnemy.asset", "Assets/Data/AI/Selectors"),
            new AssetMoveTask("HPCondition.asset", "Con_HpBelow100.asset", "Assets/Data/AI/Conditions"),
            new AssetMoveTask("DebugAction.asset", "Act_DebugLog.asset", "Assets/Data/AI/Actions")
        };

        [MenuItem(MenuPath)]
        public static void OrganizeAssets()
        {
            Debug.Log("[AIAssetOrganizer] Starting AI asset reorganization...");

            // Step 1: Ensure all destination folders exist
            foreach (string folder in RequiredFolders)
            {
                EnsureFolderExists(folder);
            }

            int successCount = 0;
            int alreadyOrganizedCount = 0;
            int failedCount = 0;

            // Step 2: Process each asset task
            foreach (var task in Tasks)
            {
                string targetFullPath = $"{task.TargetDirectory}/{task.NewFileName}";

                // Check if target asset already exists at the destination
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(targetFullPath)))
                {
                    Debug.Log($"[AIAssetOrganizer] Asset already at destination: '{targetFullPath}'. Skipping.");
                    alreadyOrganizedCount++;
                    continue;
                }

                // Locate the source asset in the project
                string sourcePath = FindAssetPath(task.SourceFileName);
                if (string.IsNullOrEmpty(sourcePath))
                {
                    Debug.LogWarning($"[AIAssetOrganizer] Source asset '{task.SourceFileName}' was not found in the project.");
                    failedCount++;
                    continue;
                }

                string assetGuid = AssetDatabase.AssetPathToGUID(sourcePath);
                string currentPath = sourcePath;

                // Rename asset if the name does not match the target name
                string newNameWithoutExt = Path.GetFileNameWithoutExtension(task.NewFileName);
                string currentNameWithoutExt = Path.GetFileNameWithoutExtension(currentPath);

                if (!currentNameWithoutExt.Equals(newNameWithoutExt, StringComparison.Ordinal))
                {
                    string renameError = AssetDatabase.RenameAsset(currentPath, newNameWithoutExt);
                    if (!string.IsNullOrEmpty(renameError))
                    {
                        Debug.LogError($"[AIAssetOrganizer] Failed to rename '{currentPath}' to '{newNameWithoutExt}': {renameError}");
                        failedCount++;
                        continue;
                    }

                    // Refresh current path using GUID to ensure accurate tracking
                    currentPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                }

                // Move asset to destination directory
                if (!currentPath.Equals(targetFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    string moveError = AssetDatabase.MoveAsset(currentPath, targetFullPath);
                    if (!string.IsNullOrEmpty(moveError))
                    {
                        Debug.LogError($"[AIAssetOrganizer] Failed to move '{currentPath}' to '{targetFullPath}': {moveError}");
                        failedCount++;
                        continue;
                    }
                }

                Debug.Log($"[AIAssetOrganizer] Successfully reorganized: '{sourcePath}' -> '{targetFullPath}'");
                successCount++;
            }

            // Step 3: Persist and refresh AssetDatabase
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            string summaryMessage = $"AI Asset Organization Complete!\n\n" +
                                   $"• Organized: {successCount}\n" +
                                   $"• Already in place: {alreadyOrganizedCount}\n" +
                                   $"• Not found / Failed: {failedCount}";

            Debug.Log($"[AIAssetOrganizer] {summaryMessage.Replace('\n', ' ')}");
            EditorUtility.DisplayDialog("Organize AI Assets", summaryMessage, "OK");
        }

        /// <summary>
        /// Recursively creates folder hierarchy under Assets if not already present.
        /// </summary>
        private static void EnsureFolderExists(string folderPath)
        {
            folderPath = folderPath.Replace('\\', '/').TrimEnd('/');
            if (AssetDatabase.IsValidFolder(folderPath))
                return;

            string[] folders = folderPath.Split('/');
            if (folders.Length == 0 || folders[0] != "Assets")
            {
                Debug.LogError($"[AIAssetOrganizer] Invalid folder path: '{folderPath}'. Must start with 'Assets'.");
                return;
            }

            string currentPath = "Assets";
            for (int i = 1; i < folders.Length; i++)
            {
                string nextPath = $"{currentPath}/{folders[i]}";
                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    string guid = AssetDatabase.CreateFolder(currentPath, folders[i]);
                    if (string.IsNullOrEmpty(guid))
                    {
                        Debug.LogError($"[AIAssetOrganizer] Failed to create folder: '{nextPath}'");
                        return;
                    }
                    Debug.Log($"[AIAssetOrganizer] Created folder: '{nextPath}'");
                }
                currentPath = nextPath;
            }
        }

        /// <summary>
        /// Searches for an asset by filename using AssetDatabase.FindAssets.
        /// </summary>
        private static string FindAssetPath(string fileNameWithExt)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileNameWithExt);
            string[] guids = AssetDatabase.FindAssets(fileNameWithoutExt);

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileName(path).Equals(fileNameWithExt, StringComparison.OrdinalIgnoreCase))
                {
                    return path;
                }
            }

            return null;
        }
    }
}

