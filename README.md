# DAB3Assignment

Beskrivelse af end points:
Alle endpoints er den pågældende basis URL tilføjet "/api/"
F.eks. https://localhost:44382/api/.

Der anbefaldes på det kraftigste at importere det vedlagte Postman collection, hvor alle nævnte request er klar til at blive brugt. 

User end points:
URL | Parameter | Response
user | | All user objekter

user/follow | {
"userFollowing":"UserId af brugeren der følger",
"userToFollow":"UserId af brugeren der følges"
} | Ingen respons

user/block  | {
	"userBlockingId":"UserId af brugeren der blockere en anden",
	"userToBlockId":"UserId af brugeren der bliver blokeret"
} | Ingen respons

user/wall | {
	"ViewerId":"Gæsten der vil se væggens id", 
	"OwnerId": "Id af ejeren af væggens der skal ses"
} | De første 10 post der matcher kriterierne fra opgavebeskrivelsen, sortere med nyeste først. 

user/feed | {
	"UserId":"Id af ejeren af den væg der skal ses"
} | De første 10 post der matcher kriterierne fra opgavebeskrivelsen, sorteret med nyeste først. 

Circle end points:
URL | Parameter | Response

circle | | ALle circle objekter

circle/Create |{
	"name":"Navnet af cirlcen",
	"usersId":["Id af brugere 1","Id af bruger 2"]
} | Den oprettet circle. Hvis en bruger ikke existere fjernes han fra listen. 

Posts end points:
URL | Parameter | Response

post | | Alle post objekter

post/createPost | {
	"Detail": {"'Text' eller 'PathToImage'" : "'Teksten' eller 'Urlen'"},
	"AuthorId": "Id af brugeren der oprettet posten",
	"CircleId" : "Id af cirklen posten skal oprettes i, eller '' for public post. ",
} | Den returnere den oprettet post. 

post/comment | {
	"PostId":"Id af posten du vil kommentere",
	"Comment":
		{
			"AuthorId" : "Id af brugeren der kommentere",
			"Text" : "Teksten kommentarer består af"
		}
} |Returnere den oprettet kommentar. 




