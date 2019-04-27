import axios from "axios";
import config from "config";

export async function get(path) {
	let result;
	let url = config.API_ENDPOINT + path;
	const options = {
		method: "GET",
		url
	};

	await axios(options)
		.then(response => {
			result = response
		})
		.catch(error => {result = error.message});

	return result;
}

export async function post(path, data) {
	let result;
	let url = config.API_ENDPOINT + path;
	const options = {
		method: "POST",
		data: data,
		url
	};

	await axios(options)
		.then(response => {
			result = response;
		})
		.catch(error => {result = error.message});

	return result;
}