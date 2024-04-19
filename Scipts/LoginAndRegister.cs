using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.Data;
using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class LoginAndRegister : MonoBehaviour
{ private string dbName = "URI=file:OW3.db";
    public TMP_InputField nickname;
    public TMP_InputField password;
    public TMP_Text info;
    public static long id;
    System.Random rnd = new System.Random();
    string[] arr = {"Mace_Bib","Senator_Lumiya","Greedo_Luminara","C-3PO_Joruus","Kyle_Jarael","Mara_Jabba",
	"Luminara_Captain","Kir_Carnor","Barriss_Durge","4-LOM_Callista","Obi-Wan_Asajj",
	"Padmé_Ulic","Yoda_Clone","Mace_Jerec","Durge_Admiral","C-3PO_Boba","Ulic_Jarael",
	"Lumiya_PROXY","Sebulba_Watto","Emperor_Qui-Gon","Zam_Durge","Zam_Kit","Ki-Adi-Mundi_Aurra","Jabba_Watto"

    ,"Bib_Natasi","Rahm_Durge","Kyp_Clone","Ki-Adi-Mundi_Chewbacca","Dengar_Sebulba","Grand_Brakiss",
	"Kyp_IG","Exar_Natasi","Grand_Padmé","Jerec_Ki-Adi-Mundi","Obi-Wan_Brakiss","General_Ben","Zayne_Zam","Natasi_4-LOM","Aayla_Clone","Zuckuss_Luminara",
	"Admiral_Plo","Grand_Princess","R2-D2_Biggs","Kyle_Yoda","Princess_Prince","Boba_Mara","PROXY_Lumiya","Emperor_Obi-Wan","Callista_Qui-Gon","Wedge_Obi-Wan"};

    // Start is called before the first frame update
    void Start()
    {
        info.gameObject.SetActive(false);
        CreateDB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateDB()
    {   //Crate DB connection
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Users (ID INTEGER PRIMARY KEY AUTOINCREMENT , NICKNAME  TEXT NOT NULL UNIQUE, PASSWORD TEXT NOT NULL , RANK INT);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Ranks (MinELO INT,MaxELO INT,RANKNAME TEXT,PRIMARY KEY(MinELO,MaxELO));";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT OR IGNORE INTO Ranks (MinELO,MaxELO,RANKNAME) " +
                    "VALUES " +
                    "(0,1000,'BRONZE')," +
                    "(1000,1500,'Silver Chariot')," +
                    "(1500,2000,'Gold Exprience')," +
                    "(2000,2500,'Star Platinum')," +
                    "(2500,3000,'Crazy Diamond'),"+
                    "(3000,'+Infinity','Master Of Puppets');";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Characters(cID INTEGER PRIMARY KEY AUTOINCREMENT, cNAME TEXT UNIQUE,HEALTH INT,DMG INT,AMMO INT);";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT OR IGNORE INTO Characters (cNAME,HEALTH,DMG,AMMO) VALUES" +
                    "('D.Va',150,20,14)," +
                    "('Ashe',200,40,12)," +
                    "('Mercy',200,20,15)," +
                    "('Winston White',200,15,40)," +
                    "('Hanzo Hasashi',200,27,12)," +
                    "('Ana',200,70,4);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Skins(sID INTEGER PRIMARY KEY AUTOINCREMENT,sNAME TEXT UNIQUE,cID INT,COST INT,FOREIGN KEY(cID) REFERENCES Characters(cID));";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT OR IGNORE INTO Skins (sNAME,cID,COST) VALUES" +
                    "('Purple Haze',5,100)," +
                    "('Doom Slayer',5,150)," +
                    "('The World',5,300)," +
                    "('Lisa Lisa',2,100)," +
                    "('Paranoid',2,150)," +
                    "('Zeppeli',2,300)," +
                    "('DIO',1,100)," +
                     "('TOTO',1,150)," +
                      "('Makise Kurisu',1,300)," +
                       "('Eagles',3,100)," +
                        "('Giga Chad',3,150)," +
                         "('Sigma',3,300)," +
                         "('Beta',6,100)," +
                         "('Amogus',6,150)," +
                         "('Casca',6,300)," +
                         "('Sniper Monkey(SIMP)',4,100)," +
                          "('Griffith',4,150)," +
                           "('Zawardo',4,300);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS INVENTORY(ID INT,sID, FOREIGN KEY(ID) REFERENCES Users(ID), FOREIGN KEY(sID) REFERENCES Skins(sID));";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS UNLOCKED(ID INT,cID, FOREIGN KEY(ID) REFERENCES Users(ID), FOREIGN KEY(cID) REFERENCES Characters(cID));";
                command.ExecuteNonQuery();
               command.CommandText = "CREATE TABLE IF NOT EXISTS MONEY(ID INT ,MONEY INT,FOREIGN KEY(ID) REFERENCES Users(ID));";
                command.ExecuteNonQuery();
                command.CommandText="CREATE TABLE IF NOT EXISTS MatchHistory(mID INTEGER PRIMARY KEY AUTOINCREMENT,DMG INT,SCORE INT,DEATH INT,cNAME TEXT);";
                command.ExecuteNonQuery();
                command.CommandText = "ALTER TABLE MatchHistory DROP COLUMN cNAME;";
                command.ExecuteNonQuery();
               command.CommandText = "CREATE TABLE IF NOT EXISTS UsersToMatch(ID INT,mID INT,FOREIGN KEY(ID) REFERENCES Users(ID),FOREIGN KEY(mID) REFERENCES MatchHistory(mID));";
               command.ExecuteNonQuery();

                for(int i = 0; i < arr.Length; i++)
                {
                    command.CommandText = "INSERT OR IGNORE  INTO Users (NICKNAME,PASSWORD,RANK) VALUES ('" + arr[i] + "',' " + arr[i] + "'," + rnd.Next(1, 6000) + ");";
                    command.ExecuteNonQuery();
                }
               









            }
            connection.Close();
        }   
    }
    public void Register()
    {
        
        string Nickname;
        string Password;
       
        bool isExist=false;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users ORDER BY ID ";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       if (reader.IsDBNull(0))
                        {
                            Debug.Log("Boþ");
                        }
                        else
                        {
                            
                            Nickname = (string)reader["NICKNAME"];
                            Password = (string)reader["PASSWORD"];
                            
                            if(nickname.text.Equals(Nickname) )
                            {
                                isExist = true;
                            }

                        }


                    }
                    reader.Close();

                }
                if(isExist)
                {
                    info.text = "This user is already exist";
                    info.gameObject.SetActive(true);

                }
                else
                {
                   
                    command.CommandText = "INSERT INTO Users (NICKNAME,PASSWORD,RANK) VALUES ('"+nickname.text+"','"+password.text+"',"+0+")";
                    command.ExecuteNonQuery();
                   
                    
                    command.CommandText = "SELECT MAX(ID) AS ID FROM Users";
                    
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = (long)reader["ID"];
                        }
                        reader.Close();
                    }
                    command.CommandText = "INSERT INTO MONEY (ID,MONEY) VALUES ("+id+",0);";
                    command.ExecuteNonQuery();
                    info.text = "Registered";
                    info.gameObject.SetActive(true);
                    SceneManager.LoadScene(4);


                }
            }
            connection.Close();
        }
        //Debug.Log(id);
    }


    public void Login()
    {
        string pass="";
       
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE NICKNAME = '" + nickname.text+"'";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pass = (string)reader["PASSWORD"];
                        id = (long)reader["ID"];


                    }
                    reader.Close();

                }


            }
            connection.Close();
        }
        if (nickname.text.Equals("")||pass.Equals(""))
        {
            info.text = "Wrong ulan";
            info.gameObject.SetActive(true);

        }
        else if(nickname.text.Equals("admin") && pass.Equals(password.text)) {
            SceneManager.LoadScene(2);
        }

       else if (password.text.Equals(pass))

        {
            SceneManager.LoadScene(4);
            Debug.Log("Baþarýlý");
        }
        else
        {
            info.text = "Wrong ulan";
            info.gameObject.SetActive(true);

        }
    }

}
   

