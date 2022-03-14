import base64
from os import access
from textwrap import indent
from urllib import response
import requests
import json

clientID = "d1e5e4e27a1d4b8090de875e6431c4e6"
clientSecret = "fe2bd6e05d434118ad8918b0a5d10e27"

authUrl = "https://accounts.spotify.com/api/token"
authHeader = {}
authData = {}

def getAccessToken(clientID, clientSecret):
    message = f"{clientID}:{clientSecret}"
    message_bytes = message.encode('ascii')
    base64_bytes = base64.b64encode(message_bytes)
    base64_message = base64_bytes.decode('ascii')

    authHeader['Authorization'] = "Basic " + base64_message
    authData['grant_type'] = "client_credentials"

    res = requests.post(authUrl, headers=authHeader, data=authData)

    responseObject = res.json()
    print(json.dumps(responseObject, indent=2))

    accessToken = responseObject['access_token']
    return accessToken

token = getAccessToken(clientID, clientSecret)
playlistID = "3VEFRGe3APwGRl4eTpMS4x?si=8954cecda15e4358"


def getPlaylistTracks(token, playlistID):
    playlistEndPoint = f"https://api.spotify.com/v1/playlists/{playlistID}"

    getHeader = {"Authorization": "Bearer " + token}

    res = requests.get(playlistEndPoint, headers=getHeader)
    playlistObject = res.json()

    return playlistObject

tracklist = getPlaylistTracks(token, playlistID)

with open('datos.json', 'w') as file:
    json.dump(tracklist, file, indent=4)

for t in tracklist['tracks']['items']:
    print("--------------------------------")
    for a in t['track']['artists']:
        print(a['name'])

    nombreCancion = t['track']['name']
    print(nombreCancion)





