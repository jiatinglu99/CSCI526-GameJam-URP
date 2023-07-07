import matplotlib.pyplot as plt
import firebase_admin
from firebase_admin import credentials
from firebase_admin import db
import math

import json


def retrieve_data():
	#https://jomandterry-3569d-default-rtdb.firebaseio.com/.json

	# Fetch the service account key JSON file contents
	cred = credentials.Certificate('cs526-48acc-firebase-adminsdk-t9ums-64bc818726.json')
	# Initialize the app with a service account, granting admin privileges
	firebase_admin.initialize_app(cred, {
	    'databaseURL': "https://jomandterry-3569d-default-rtdb.firebaseio.com/"
	})

	ref = db.reference('')
	print(ref.get())

	data = ""
	return data


def read_json():
	json_file = 'jomandterry-3569d-default-rtdb-export-2.json'

	with open(json_file) as json_data:
	    data = json.load(json_data)

	# print(data)
	return data


def parse_data(data):
	clean_data = dict()

	# switches dict to use userID as key
	for database_id, values in data.items():
		user_id = values["userID"]

		if user_id not in clean_data:
			clean_data[user_id] = dict()
			clean_data[user_id]["highestCompletedLevel"] = values["highestCompletedLevel"]
			# print(clean_data[user_id]["highestCompletedLevel"])
			clean_data[user_id]["timeSpent"] = values["timeSpent"]
			clean_data[user_id]["timesRetried"] = values["timesRetried"]

		if values["highestCompletedLevel"] > clean_data[user_id]["highestCompletedLevel"]:
			# clean_data[user_id]["highestCompletedLevel"]
			clean_data[user_id]["highestCompletedLevel"] = min(values["highestCompletedLevel"],3) # 3 should be changed to the max number of levels

	print(clean_data)

	return clean_data




def generate_graphs(data):

	highest_completed_level = {0: 0, 1: 0, 2: 0, 3:0}

	# graph of the highest completed level
	for key, values in data.items():
		highest_completed_level[values["highestCompletedLevel"]] += 1
	
	level = highest_completed_level.keys()
	num_players = highest_completed_level.values()

	fig = plt.figure(figsize = (10, 5))
	plt.bar(level, num_players)

	plt.xlabel("Highest Completed Level")
	plt.ylabel("Number of Players")
	plt.title("The Number of Players Whose Highest Completed Level Was X")
	plt.xticks([0, 1, 2, 3])
	plt.show()


if __name__ == '__main__':
	# data = retrieveData()
	raw_data = read_json()
	clean_data = parse_data(raw_data)
	generate_graphs(clean_data)





