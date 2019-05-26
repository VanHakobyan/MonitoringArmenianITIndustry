import {call, put, select} from "redux-saga/effects";
import * as profile from "store/actions/profile";
import {
	currentProfileDataSelector,
	crossProfileDataSelector
} from "store/selectors/profile";
import {get} from "services/api";
import _ from "lodash";

let socialDetails = {
	github: {
		reverse: "linkedin",
		crossName: "GithubProfile",
		reverseCrossName: "LinkedinProfile",
	},
	linkedin: {
		reverse: "github",
		crossName: "LinkedinProfile",
		reverseCrossName: "GithubProfile",
	},
};

function* getCrossProfiles() {
	let result = yield call(get, "/crossmap/GetAllCrossProfiles");
	if(result.status < 400) {
		return result.data;
	} else {
		return [];
	}
}
// 3 replace {id}
function checkCrossMap(crossData, social, id) {
	return _.find(crossData, o => {
		return o[socialDetails[social]["crossName"]].Id === id;
	});
}

export function* getProfileData(api) {
	try {
		let crossProfiles = yield select(crossProfileDataSelector);
		if(!crossProfiles) {
			crossProfiles = yield getCrossProfiles();
		}
		if(crossProfiles.length) {
			yield put(profile.succeededCrossProfile(crossProfiles));
		} else {
			yield put(profile.failedCrossProfile("no cross data"))
		}
		let profileData = {};
		let result = yield call(get, `${api.social}/${api.id}`);
		if(result.status < 400){
			profileData = {
				main: {},
				second: {},
			};
			profileData.main.name = api.social;
			profileData.main.info = result.data;
			let crossElement = checkCrossMap(crossProfiles, api.social, parseInt(api.id));
			if(crossElement){
				let url = `${socialDetails[api.social]["reverse"]}/${crossElement[socialDetails[api.social]["reverseCrossName"]].Id}`;
				let res = yield call(get, url);
				if(res.status < 400) {
					profileData.second.name = socialDetails[api.social]["reverse"];
					profileData.second.info = res.data;
				}
			}
			yield put(profile.succeededProfile(profileData));
		}
	} catch(e) {
		yield put(profile.failedProfile(e.message));
	}
}