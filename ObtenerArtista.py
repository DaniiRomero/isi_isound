import requests
import base64
import json
import urllib


clientId = '6fb9230e20624a22af4a1100a10246f4'
clientSecret = '3ed963a437a34b04a855b7ead6c82746'

# Step 1 - Authorization 
url = "https://accounts.spotify.com/api/token"
headers = {}
data = {}

# Encode as Base64
message = f"{clientId}:{clientSecret}"
messageBytes = message.encode('ascii')
base64Bytes = base64.b64encode(messageBytes)
base64Message = base64Bytes.decode('ascii')
headers['Authorization'] = f"Basic {base64Message}"
data['grant_type'] = "client_credentials"
r = requests.post(url, headers=headers, data=data)
token = r.json()['access_token']




url = "https://api.spotify.com/v1/search?"
headers = {
    "Authorization": "Bearer " + token
}
tipe = "artist"
limit = 1
q = input("Introduce nombre artista:\n")
url = url + urllib.parse.urlencode({"q":q, "type":tipe, "limit":limit})

res = requests.get(url=url, headers=headers)
idArtista = (res).json()['artists']['items'][0]['id']
print(idArtista)
