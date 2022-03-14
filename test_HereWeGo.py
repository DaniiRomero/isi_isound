import requests
import json
from turtle import distance
import urllib
import shutil

"""CONEXION A LA API DE HEREWEGO MEDIANTE LA KEY Y LAS RUTAS INICIALES"""

key = "lCDUb5Ol6Xos1uRifLp-DFTlgbuXjyMDn73PSETQXhc"
api_ruta = "https://router.hereapi.com/v8/routes?"
api_geoloc = "https://geocode.search.hereapi.com/v1/geocode?"


transporte = "car"
retur = "summary"

"""GEOLOCALIZACION DE LA DIRECCION INTRODUCIDA PARA OBTENER COORDENADAS GEOGRAFICAS"""

initPoint = input("Introduce punto origen:")
finalPoint = input("Introduce punto final:")

urlInit = api_geoloc + urllib.parse.urlencode({"q":initPoint,"apikey":key})
urlFinal = api_geoloc + urllib.parse.urlencode({"q":finalPoint, "apikey":key})

json_dataInit = requests.get(urlInit).json()
json_dataFinal = requests.get(urlFinal).json()
latInit = json_dataInit['items'][0]['position']['lat']
longInit = json_dataInit['items'][0]['position']['lng']

latFinal = json_dataFinal['items'][0]['position']['lat']
longFinal = json_dataFinal['items'][0]['position']['lng']
origen = str(latInit)+","+str(longInit)
destino = str(latFinal)+","+str(longFinal)


"""OBTENCIÓN DE DATOS INICIALES DE LA RUTA"""
urlRuta = api_ruta + urllib.parse.urlencode({"transportMode":transporte, "origin":origen, "destination":destino, "return":retur, "apikey":key})
json_dataRuta = requests.get(urlRuta).json()

duracion_km = str(json_dataRuta['routes'][0]['sections'][0]['summary']['length'] / 1000) + " km"
duracion_time = json_dataRuta['routes'][0]['sections'][0]['summary']['duration']
print(duracion_km)

horas=int(duracion_time/3600)
duracion_time-=horas*3600
minutos=int(duracion_time/60)
duracion_time-=minutos*60
print("%s horas : %s minutos : %s segundos" % (horas,minutos,duracion_time))