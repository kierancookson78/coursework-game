using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic; // Required for Dictionary

[System.Serializable]
public class UserData
{
    public string username;
    public int highScore = 0;
}

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }

    private Dictionary<string, UserData> users = new Dictionary<string, UserData>();
    private string saveFilePath;
    private UserData currentUser;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/users.dat"; // Changed file name
        LoadUsers();
    }

    public bool CreateUser(string username)
    {
        if (users.ContainsKey(username))
        {
            Debug.LogWarning("User with username '" + username + "' already exists.");
            return false; // User already exists
        }

        UserData newUser = new UserData { username = username, highScore = 0 };
        users.Add(username, newUser);
        SaveUsers();
        currentUser = newUser;
        return true; // User created successfully
    }

    public bool LoadUser(string username)
    {
        if (users.ContainsKey(username))
        {
            currentUser = users[username];
            return true;
        }
        else
        {
            Debug.LogWarning("User with username '" + username + "' does not exist.");
            return false;
        }
    }

    public void UpdateHighScore(int newScore)
    {
        if (currentUser != null && newScore > currentUser.highScore)
        {
            currentUser.highScore = newScore;
            SaveUsers();
        }
    }

    public int GetHighScore()
    {
        if (currentUser != null)
        {
            return currentUser.highScore;
        }
        else
        {
            Debug.LogWarning("No user loaded.");
            return 0;
        }
    }

    public string GetUsername()
    {
        if (currentUser != null)
        {
            return currentUser.username;
        }
        else
        {
            Debug.LogWarning("No user loaded.");
            return "";
        }
    }

    private void SaveUsers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFilePath, FileMode.Create);

        formatter.Serialize(stream, users);
        stream.Close();
    }

    private void LoadUsers()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(saveFilePath, FileMode.Open);

                users = formatter.Deserialize(stream) as Dictionary<string, UserData>;
                stream.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading users: " + e.Message);
                users = new Dictionary<string, UserData>(); // Reset to empty if loading fails.
            }
        }
        else
        {
            Debug.Log("No user data file found.");
            users = new Dictionary<string, UserData>();
        }
    }
}
