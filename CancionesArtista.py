from turtle import distance
import requests
import urllib
import shutil

import spotipy
from spotipy.oauth2 import SpotifyClientCredentials

sp = spotipy.Spotify(auth_manager=SpotifyClientCredentials(client_id="d1e5e4e27a1d4b8090de875e6431c4e6", client_secret="fe2bd6e05d434118ad8918b0a5d10e27"))

q = input("Introduce el artista: ")
limit = 25


results = sp.search(q, limit)

for idx, album in enumerate(results['tracks']['items']):
    print(idx, album['name'])