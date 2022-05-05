import json
import os
import sys
import requests
from flask import Flask, jsonify
#from urllib import parse, error
from bs4 import BeautifulSoup

app = Flask(__name__)


def FormatJson(status, message):
	json_response = {
		'status':status,
		'message':message
	}
	return json_response

@app.route('/Request/Movie/<title>',  methods=['GET'])
def GetMovie(title):
	source = "www.springfieldspringfield.co.uk"
	url = "http://{0}/movie_scripts.php?search={1}".format(source, title)
	try:
		html = requests.get(url).content
	except:
		error_message = "Unable to connect to: {0}".format(source)
		return FormatJson("ERROR", error_message)
	try:
		soup = BeautifulSoup(html, 'html.parser').find('a', attrs={'class':'btn btn-dark btn-sm'})
		url = 'https://www.springfieldspringfield.co.uk/' + soup.get('href')
		html = requests.get(url).content
	except:
		error_message = "No reults for: '{0}' found on: {1}.".format(title, source)
		return FormatJson("ERROR", error_message)
	try:				
		soup = BeautifulSoup(html, 'html.parser').find('div', attrs={'class':'scrolling-script-container'})
		parsed_text = soup.get_text()
		soup = soup.find_all('br')
		for textbox in soup:
			parsed_text += textbox.get_text()	
		return FormatJson("OK", parsed_text)
	except:
		error_message = "A movie script was found but could not be parsed."
		return FormatJson("ERROR", error_message)


@app.route('/Request/Book/<title>', methods=['GET'])
def GetBook(title):
	source = "www.authorama.com"
	url = "http://{0}/book/{1}.html".format(source, title)
	try:
		html = requests.get(url).content
	except:
		error_message = "Unable to connect to: {0}".format(source)
		return FormatJson("ERROR", error_message)
	try:
		soup = BeautifulSoup(html, 'html.parser')
		parsed_text = soup.find('h1').get_text()
		soup = soup.find('div', attrs={'class':'content'}).find_all('p')
		for textbox in soup:
			parsed_text += textbox.get_text()
		if (parsed_text == "Authorama\nPublic Domain BooksThe file you were looking for cannot be found.\nIt might have moved, so please take a look at the Authorama homepage."):
			error_message = "No results for: '{0}' found on: {1}.".format(title, source)
			return FormatJson("ERROR", error_message)			
		return FormatJson("OK", parsed_text)
	except:
		error_message = "No results for: '{0}' found on: {1}.".format(title, source)
		return FormatJson("ERROR", error_message)


@app.route('/Request/Wikipedia/<title>', methods=['GET'])
def GetWiki(title):
	source = "en.wikipedia.org"
	url = "https://{0}/wiki/{1}".format(source, title)
	try:
		html = requests.get(url).content
	except:
		error_message = "Unable to connect to: {0}".format(source)
		return FormatJson("ERROR", error_message)
	try:
		soup = BeautifulSoup(html, 'html.parser').find('div', attrs={'class':'mw-body-content'})
		parsed_text = ""
		for textbox in soup.find('div', attrs={'class':'mw-parser-output'}).find_all("p"):
			parsed_text += textbox.text
		return FormatJson("OK", parsed_text)
	except:
		error_message = "Unable to parse the results properly."
		return FormatJson("ERROR", error_message)


@app.route('/Request/Song/<title>', methods=['GET'])
def GetSong(title):
	source = "search.azlyrics.com"
	url = "https://{0}/search.php?q={1}".format(source, title)
	try:
		html = requests.get(url).content
	except:
		error_message = "Unable to connect to: {0}".format(source)
		return FormatJson("ERROR", error_message)		
	try:
		soup = BeautifulSoup(html, 'html.parser').find('div', attrs={'class':'container main-page'})
		soup = soup.find('div', attrs={'class':'col-xs-12'}).find('table', attrs={'class':'table table-condensed'})
		url = soup.find('a', href=True).get('href')
	except:
		error_message = "No results for: '{0}' found on: {1}".format(title, source)
		return FormatJson("ERROR", error_message)
	try:
		html = requests.get(url).content
	except:
		error_message = "A matching song was found but could not be opened."
		return FormatJson("ERROR", error_message)
	try:
		soup = BeautifulSoup(html, 'html.parser').find('div', attrs={'class':'container main-page'})
		soup = soup.find('div', attrs={'class':'col-xs-12 col-lg-8 text-center'})
		soup = soup.find('div', attrs={'class':'ringtone'}).findNext('div')
		parsed_text = soup.get_text()
		soup = soup.find_all('br')
		for textbox in soup:
			parsed_text += textbox.get_text()
		return FormatJson("OK", parsed_text)
	except:
		error_message = "A song was found but could not be parsed."
		return FormatJson("ERROR", error_message)
    	

if __name__ == "__main__":
	app.run()
