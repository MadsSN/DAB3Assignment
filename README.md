# The Social Network - Gruppe 38
---

## Prerequisites
1. Installer MongoDB ([Download her](https://docs.mongodb.com/manual/administration/install-community/ "Download her"))
2. Installer Postman ([Download her](https://www.getpostman.com/ "Download her"))
3. Importer den vedlagte DAB3.postman_collection.json i Postman

Ved at installere postman og importere den nævnte collection, så har du alle de endpoints som beskrives nedenfor opsat og klar til brug.

## Usage
Alle endpoints har samme basis URL, hvor selve portnummeret i URLen dog afhænger af din maskine. F.eks. kan den være https://localhost:44382/api/, hvor 44382 er det, som afhænger af din egen maskine.
Der opfordres igen til, at du benytter Postman og den vedlagte collection, som nævnt under prerequisites.

1. Åbn solution i Visual Studio
2. Tryk CTRL+F5 for at afvikle solution
---
## Se alle brugere
### URL
https://localhost:44382/api/user
### Parametre
Ingen.
### Respons
Alle brugere.

---
## Følg bruger
### URL
https://localhost:44382/api/user/follow
### Parametre
```json
{ 
    "userFollowing":"UserId af brugeren der følger", 
    "userToFollow":"UserId af brugeren der følges" 
}
```
### Respons
Ingen.

---
## Blokér bruger
### URL
https://localhost:44382/api/user/block
### Parametre
```json
{ 
    "userBlockingId":"UserId af brugeren der ønsker at blokere", 
    "userToBlockId":"UserId af brugeren der skal blokeres" 
}
```
### Respons
Ingen.

---
## Se anden brugers væg
### URL
https://localhost:44382/api/user/wall
### Parametre
```json
{ 
    "ViewerId":"UserId af brugeren der vil se væggen", 
    "OwnerId": "UserId af den bruger hvis væg der skal ses"
}
```
### Respons
De første 10 posts der matcher kriterierne fra opgavebeskrivelsen.
Sorteret med nyeste først.

---
## Se eget feed
### URL
https://localhost:44382/api/user/feed
### Parametre
```json
{ 
    "UserId":"Id af den bruger, hvis feed der skal vises" 
}
```
### Respons
De første 10 posts der matcher kriterierne fra opgavebeskrivelsen.
Sorteret med nyeste først.

---
## Se alle circles
### URL
https://localhost:44382/api/circle
### Parametre
Ingen.
### Respons
Alle circles.

---
## Opret circle
### URL
https://localhost:44382/api/circle/create
### Parametre
```json
{ 
    "name":"Navnet på circle", 
    "usersId": [
        "Id af bruger 1",
        "Id af bruger 2"
    ] 
} 
```
### Respons
Den oprettede circle.

---
## Se alle posts
### URL
https://localhost:44382/api/post
### Parametre
Ingen.
### Respons
Alle posts.

---
## Opret post
### URL
https://localhost:44382/api/post/createpost
### Parametre
```json
{ 
    "Detail": {
        "Text" : "Tekst",
        "PathToImage": "Url"
    }, 
    "AuthorId": "Id af brugeren der opretter posten", 
    "CircleId" : "Id af cirklen posten skal oprettes i (Efterlad blank for en public post)" 
}
```
### Respons
Den oprettede post.

---
## Opret comment på post
### URL
https://localhost:44382/api/post/comment
### Parametre
```json
{ 
    "PostId":"Id af den post du vil kommentere", 
    "Comment": { 
        "AuthorId" : "Id af brugeren der kommentere", 
        "Text" : "Kommentaren" 
    } 
}
```
### Respons
Den oprettede comment.

---