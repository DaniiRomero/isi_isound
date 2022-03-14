from ast import Expression
from email.mime import application
from tkinter import SINGLE
import requests
import json
from turtle import distance
import urllib
import shutil
import spotipy
from spotipy.oauth2 import SpotifyClientCredentials

client_id = '6fb9230e20624a22af4a1100a10246f4';
clientSecret = '3ed963a437a34b04a855b7ead6c82746'

client_credentials_manager = SpotifyClientCredentials(client_id, clientSecret)
sp = spotipy.Spotify(client_credentials_manager=client_credentials_manager)



bad_bunny_uri = 'spotify:artist:4q3ewBCX7sLwd24euuV69X'
spotify = spotipy.Spotify(client_credentials_manager=SpotifyClientCredentials())


results = spotify.artist_albums(bad_bunny_uri, album_type='album')
albums = results['items']
while results['next']:
    results = spotify.next(results)
    albums.extend(results['items'])
for album in albums:
    print(album['name'])
results2 = spotify.artist_top_tracks(bad_bunny_uri)
print()
for track in results2['tracks'][:5]:
   
    print('track    : ' + track['name'])
    print('audio    : ' + track['preview_url'])
    print('cover art: ' + track['album']['images'][0]['url'])
    print()

