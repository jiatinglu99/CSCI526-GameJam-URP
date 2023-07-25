import matplotlib.pyplot as plt
# import firebase_admin
#from firebase_admin import credentials
# from firebase_admin import db
import math

import json
import ast

import numpy as np


def retrieve_data():
	#https://jomandterry-3569d-default-rtdb.firebaseio.com/.json

	# Fetch the service account key JSON file contents
	# cred = credentials.Certificate('cs526-48acc-firebase-adminsdk-t9ums-64bc818726.json')
	# Initialize the app with a service account, granting admin privileges
	# firebase_admin.initialize_app(cred, {
	#    'databaseURL': "https://jomandterry-3569d-default-rtdb.firebaseio.com/"
	# })

	#ref = db.reference('')
	# print(ref.get())

	data = ""
	return data


def read_json():
	json_file = 'jomandterry-3569d-default-rtdb-export-final-reduced.json'

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
			try:
				clean_data[user_id]["cornersData"] = ast.literal_eval(values["cornersData"])
			except SyntaxError:
				# print("SyntaxError on:", user_id)
				clean_data[user_id]["cornersData"] = {"0-0":0,"0-1":0,"0-2":0,"0-3":0,"1-0":0,"1-1":0,"1-2":0,"1-3":0,"2-0":0,"2-1":0,"2-2":0,"2-3":0,"3-0":0,"3-1":0,"3-2":0,"3-3":0,"4-0":0,"4-1":0,"4-2":0,"4-3":0,"5-0":0,"5-1":0,"5-2":0,"5-3":0,"6-0":0,"6-1":0,"6-2":0,"6-3":0,"7-0":0,"7-1":0,"7-2":0,"7-3":0}

			clean_data[user_id]["timeSpent"] = values["timeSpent"]
			clean_data[user_id]["timesRetried"] = values["timesRetried"]

		# totally goofed this one up :(
		# I think it can be fixed by using the corners data
		# if values["highestCompletedLevel"] > clean_data[user_id]["highestCompletedLevel"]:
		# 	# clean_data[user_id]["highestCompletedLevel"]
		# 	clean_data[user_id]["highestCompletedLevel"] = min(values["highestCompletedLevel"],5) # 5 should be changed to the max number of levels

		# print(type(clean_data[user_id]["cornersData"]))

		# here's the fix. It works! :)
		for key, value in clean_data[user_id]["cornersData"].items():
			if value > 0:
				level = int(key[0])
				if level > clean_data[user_id]["highestCompletedLevel"]:
					clean_data[user_id]["highestCompletedLevel"] = level


		for i in range(len(values["timeSpent"])):
			if values["timeSpent"][i] > clean_data[user_id]["timeSpent"][i]:
				clean_data[user_id]["timeSpent"][i] = values["timeSpent"][i]

		for i in range(len(values["timesRetried"])):
			if values["timesRetried"][i] > clean_data[user_id]["timesRetried"][i]:
				clean_data[user_id]["timesRetried"][i] = values["timesRetried"][i]

	# for uID in clean_data.keys():
	# 	print(clean_data[uID]["timeSpent"])
	# print(clean_data)

	return clean_data




def generate_highest_comp_level_graph(data):

	# highest_completed_level = {0: 0, 1: 0, 2: 0, 3:0, 4:0, 5:0}

	highest_completed_level = [0, 0, 0, 0, 0, 0]

	# graph of the highest completed level
	for key, values in data.items():
		highest_completed_level[values["highestCompletedLevel"]] += 1
	
	# level = highest_completed_level.keys()
	# num_players = highest_completed_level.values()
	# print(num_players)

	# print(highest_completed_level)

	fig = plt.figure(figsize = (10, 5))
	bar_container = plt.bar([1, 2, 3, 4, 5], highest_completed_level[1:])

	plt.xlabel("Highest Completed Level")
	plt.ylabel("Number of Players")
	plt.title("The Number of Players Whose Highest Completed Level Was X")
	plt.xticks([1, 2, 3, 4, 5])
	plt.bar_label(bar_container)
	# plt.show()

	return highest_completed_level

def generate_time_spend_graph(data):

	sum_of_times = [0, 0, 0, 0, 0, 0]
	num_attempts = [0, 0, 0, 0, 0, 0]
	avg_time 	 = [0, 0, 0, 0, 0]

	for key, values in data.items():
		for i in range(len(values["timeSpent"])):
			if values["timeSpent"][i] > 0:
				sum_of_times[i] += values["timeSpent"][i]
				num_attempts[i] += 1

	# print(sum_of_times)
	# print(num_attempts)

	for i in range(1, len(sum_of_times)):
		avg_time[i-1] = round(sum_of_times[i] / num_attempts[i],1)

	# print(avg_time)

	fig = plt.figure(figsize = (10, 5))
	bar_container = plt.bar([1, 2, 3, 4, 5], avg_time)

	plt.xlabel("Level")
	plt.ylabel("Time (seconds)")
	plt.title("Average Time Spent Per Level")
	plt.xticks([1, 2, 3, 4, 5])
	plt.bar_label(bar_container)
	# plt.show()

	return avg_time


def generate_times_retried_graph(data):

	times_retried = [0, 0, 0, 0, 0, 0]

	ts_List = []

	for uID in data.keys():
		# print(clean_data[uID]["timeSpent"])
		ts_List.append(clean_data[uID]["timeSpent"])

	for i in range(1, len(ts_List)):
		# print(ts_List[i])
		if ts_List[i] == ts_List[i-1]:
			# found = False
			for j in range(1, len(ts_List[i])):
				if ts_List[i][j] == 0:
					times_retried[j-1] += 1
					# found = True
					break
				# if found:
				# 	j = len(ts_List[i]) + 1
				# 	print(j)
	times_retried = [times_retried[0], times_retried[1], times_retried[2] + times_retried[4], times_retried[5]]

	# print(times_retried)

	fig = plt.figure(figsize = (10, 5))
	bar_container = plt.bar([2, 3, 4, 5], times_retried)

	plt.xlabel("Level")
	plt.ylabel("Number of Reattempts")
	plt.title("Number of Times Each Level was Reattempted")
	plt.xticks([2, 3, 4, 5])
	plt.yticks([5, 10, 15, 20, 25])
	plt.bar_label(bar_container)
	# plt.show()

	return times_retried


def generate_corners_graphs(data, level): # level to spawn
	corners_visited = [0, 0, 0, 0]
	# "3-0": 0, "3-1": 0, "3-2": 0, "3-3": 0

	for key, values in data.items():
		corners_visited[0] += values["cornersData"]["3-0"]
		corners_visited[1] += values["cornersData"]["3-1"]
		corners_visited[2] += values["cornersData"]["3-2"]
		corners_visited[3] += values["cornersData"]["3-3"]

	fig = plt.figure(figsize=(10, 5))
	bar_container = plt.bar([1, 2, 3, 4], corners_visited[:])

	plt.xlabel("Times Corners Visited")
	plt.ylabel("Number of Players")
	plt.title("Times Corners Visited")
	x = np.array([1, 2, 3, 4])
	x_ticks_labels = ['Lower right', 'Upper right', 'Lower left', 'Upper left']
	plt.xticks(x, x_ticks_labels)

	plt.bar_label(bar_container)
	plt.show()

	corners = [corners_visited[0], corners_visited[1], corners_visited[2] + corners_visited[3]]

	return corners


def generate_avg_attempts_per_complete(highest_completed_level, times_retried):
	total_completions = []
	avg_completions = [0, 0, 0, 0]
	highest_completed_level = highest_completed_level[2:]

	for i in range(len(highest_completed_level)):
		total_completions.append(sum(highest_completed_level[i:]))

	for i in range(len(total_completions)):
		times_retried[i] += total_completions[i]
		avg_completions[i] = round(times_retried[i] / total_completions[i],1)


	fig = plt.figure(figsize = (10, 5))
	bar_container = plt.bar([2, 3, 4, 5], avg_completions)

	plt.xlabel("Level")
	plt.ylabel("Avg Number of Attempts")
	plt.title("Average Number of Attempts per Level Completion")
	plt.xticks([2, 3, 4, 5])
	plt.bar_label(bar_container)
	# plt.yticks([5, 10, 15, 20, 25])
	# plt.show()



if __name__ == '__main__':
	# data = retrieveData()
	raw_data = read_json()
	clean_data = parse_data(raw_data)

	highest_completed_level = generate_highest_comp_level_graph(clean_data)
	avg_time = generate_time_spend_graph(clean_data)
	times_retried = generate_times_retried_graph(clean_data)
	corners = generate_corners_graphs(clean_data, 3)
	generate_avg_attempts_per_complete(highest_completed_level, times_retried)


	# plt.show()







