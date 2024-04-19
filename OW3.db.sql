BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Users" (
	"ID"	INTEGER,
	"NICKNAME"	TEXT NOT NULL UNIQUE,
	"PASSWORD"	TEXT NOT NULL,
	"RANK"	INT,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Ranks" (
	"MinELO"	INT,
	"MaxELO"	INT,
	"RANKNAME"	TEXT,
	PRIMARY KEY("MinELO","MaxELO")
);
CREATE TABLE IF NOT EXISTS "Characters" (
	"cID"	INTEGER,
	"cNAME"	TEXT UNIQUE,
	"HEALTH"	INT,
	"DMG"	INT,
	"AMMO"	INT,
	PRIMARY KEY("cID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Skins" (
	"sID"	INTEGER,
	"sNAME"	TEXT UNIQUE,
	"cID"	INT,
	"COST"	INT,
	FOREIGN KEY("cID") REFERENCES "Characters"("cID"),
	PRIMARY KEY("sID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "INVENTORY" (
	"ID"	INT,
	"sID"	,
	FOREIGN KEY("ID") REFERENCES "Users"("ID"),
	FOREIGN KEY("sID") REFERENCES "Skins"("sID")
);
CREATE TABLE IF NOT EXISTS "UNLOCKED" (
	"ID"	INT,
	"cID"	,
	FOREIGN KEY("cID") REFERENCES "Characters"("cID"),
	FOREIGN KEY("ID") REFERENCES "Users"("ID")
);
CREATE TABLE IF NOT EXISTS "MONEY" (
	"ID"	INT,
	"MONEY"	INT,
	FOREIGN KEY("ID") REFERENCES "Users"("ID")
);
CREATE TABLE IF NOT EXISTS "MatchHistory" (
	"mID"	INTEGER,
	"DMG"	INT,
	"SCORE"	INT,
	"DEATH"	INT,
	PRIMARY KEY("mID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "UsersToMatch" (
	"ID"	INT,
	"mID"	INT,
	FOREIGN KEY("ID") REFERENCES "Users"("ID"),
	FOREIGN KEY("mID") REFERENCES "MatchHistory"("mID")
);
INSERT INTO "Users" ("ID","NICKNAME","PASSWORD","RANK") VALUES (1,'Mace_Bib',' Mace_Bib',5465),
 (2,'Senator_Lumiya',' Senator_Lumiya',4292),
 (3,'Greedo_Luminara',' Greedo_Luminara',4907),
 (4,'C-3PO_Joruus',' C-3PO_Joruus',738),
 (5,'Kyle_Jarael',' Kyle_Jarael',616),
 (6,'Mara_Jabba',' Mara_Jabba',2675),
 (7,'Luminara_Captain',' Luminara_Captain',3009),
 (8,'Kir_Carnor',' Kir_Carnor',5036),
 (9,'Barriss_Durge',' Barriss_Durge',5987),
 (10,'4-LOM_Callista',' 4-LOM_Callista',5120),
 (11,'Obi-Wan_Asajj',' Obi-Wan_Asajj',5196),
 (12,'Padmé_Ulic',' Padmé_Ulic',1467),
 (13,'Yoda_Clone',' Yoda_Clone',529),
 (14,'Mace_Jerec',' Mace_Jerec',2068),
 (15,'Durge_Admiral',' Durge_Admiral',812),
 (16,'C-3PO_Boba',' C-3PO_Boba',2682),
 (17,'Ulic_Jarael',' Ulic_Jarael',657),
 (18,'Lumiya_PROXY',' Lumiya_PROXY',5157),
 (19,'Sebulba_Watto',' Sebulba_Watto',5472),
 (20,'Emperor_Qui-Gon',' Emperor_Qui-Gon',362),
 (21,'Zam_Durge',' Zam_Durge',2888),
 (22,'Zam_Kit',' Zam_Kit',2799),
 (23,'Ki-Adi-Mundi_Aurra',' Ki-Adi-Mundi_Aurra',4977),
 (24,'Jabba_Watto',' Jabba_Watto',126),
 (25,'Bib_Natasi',' Bib_Natasi',5068),
 (26,'Rahm_Durge',' Rahm_Durge',2711),
 (27,'Kyp_Clone',' Kyp_Clone',4781),
 (28,'Ki-Adi-Mundi_Chewbacca',' Ki-Adi-Mundi_Chewbacca',4060),
 (29,'Dengar_Sebulba',' Dengar_Sebulba',3519),
 (30,'Grand_Brakiss',' Grand_Brakiss',1334),
 (31,'Kyp_IG',' Kyp_IG',943),
 (32,'Exar_Natasi',' Exar_Natasi',4038),
 (33,'Grand_Padmé',' Grand_Padmé',4379),
 (34,'Jerec_Ki-Adi-Mundi',' Jerec_Ki-Adi-Mundi',5504),
 (35,'Obi-Wan_Brakiss',' Obi-Wan_Brakiss',4582),
 (36,'General_Ben',' General_Ben',3530),
 (37,'Zayne_Zam',' Zayne_Zam',3464),
 (38,'Natasi_4-LOM',' Natasi_4-LOM',4078),
 (39,'Aayla_Clone',' Aayla_Clone',1486),
 (40,'Zuckuss_Luminara',' Zuckuss_Luminara',1028),
 (41,'Admiral_Plo',' Admiral_Plo',1528),
 (42,'Grand_Princess',' Grand_Princess',5333),
 (43,'R2-D2_Biggs',' R2-D2_Biggs',2605),
 (44,'Kyle_Yoda',' Kyle_Yoda',5586),
 (45,'Princess_Prince',' Princess_Prince',3032),
 (46,'Boba_Mara',' Boba_Mara',564),
 (47,'PROXY_Lumiya',' PROXY_Lumiya',4409),
 (48,'Emperor_Obi-Wan',' Emperor_Obi-Wan',2725),
 (49,'Callista_Qui-Gon',' Callista_Qui-Gon',4375),
 (50,'Wedge_Obi-Wan',' Wedge_Obi-Wan',1397),
 (51,'a','a',-20);
INSERT INTO "Ranks" ("MinELO","MaxELO","RANKNAME") VALUES (0,1000,'BRONZE'),
 (1000,1500,'Silver Chariot'),
 (1500,2000,'Gold Exprience'),
 (2000,2500,'Star Platinum'),
 (2500,3000,'Crazy Diamond'),
 (3000,'+Infinity','Master Of Puppets');
INSERT INTO "Characters" ("cID","cNAME","HEALTH","DMG","AMMO") VALUES (1,'D.Va',150,20,14),
 (2,'Ashe',200,40,12),
 (3,'Mercy',200,20,15),
 (4,'Winston White',200,15,40),
 (5,'Hanzo Hasashi',200,27,12),
 (6,'Ana',200,70,4);
INSERT INTO "Skins" ("sID","sNAME","cID","COST") VALUES (1,'Purple Haze',5,100),
 (2,'Doom Slayer',5,150),
 (3,'The World',5,300),
 (4,'Lisa Lisa',2,100),
 (5,'Paranoid',2,150),
 (6,'Zeppeli',2,300),
 (7,'DIO',1,100),
 (8,'TOTO',1,150),
 (9,'Makise Kurisu',1,300),
 (10,'Eagles',3,100),
 (11,'Giga Chad',3,150),
 (12,'Sigma',3,300),
 (13,'Beta',6,100),
 (14,'Amogus',6,150),
 (15,'Casca',6,300),
 (16,'Sniper Monkey(SIMP)',4,100),
 (17,'Griffith',4,150),
 (18,'Zawardo',4,300);
INSERT INTO "MONEY" ("ID","MONEY") VALUES (51,40);
INSERT INTO "MatchHistory" ("mID","DMG","SCORE","DEATH") VALUES (1,5558,5,5),
 (2,5925,5,5),
 (3,8487,8,2),
 (4,1536,1,9),
 (5,6643,6,4);
INSERT INTO "UsersToMatch" ("ID","mID") VALUES (51,1),
 (51,2),
 (51,3),
 (51,4),
 (51,5);
COMMIT;
