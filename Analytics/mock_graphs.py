import matplotlib.pyplot as plt


def mock():

	# data

	# highest_completed_level = {0: 0, 1: 0, 2: 0, 3:0}

	# # graph of the highest completed level
	# for key, values in data.items():
	# 	highest_completed_level[values["highestCompletedLevel"]] += 1
	
	level = ["Midpoint", "Finish"]
	num_players = [14.1, 6.7]

	fig = plt.figure(figsize = (10, 5))
	plt.bar(level, num_players)

	plt.xlabel("Level")
	plt.ylabel("Attempts")
	plt.title("Median Time Between Completing Puzzle Objectives on Level 2")
	# plt.xticks()
	plt.show()

mock()