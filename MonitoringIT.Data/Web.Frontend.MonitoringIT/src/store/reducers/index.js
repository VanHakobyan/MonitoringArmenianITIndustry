import { combineReducers } from "redux";
import githubProfiles from "store/reducers/githubProfiles";
import linkedinProfiles from "store/reducers/linkedinProfiles";
import companies from "store/reducers/companies";
import jobs from "store/reducers/jobs";
import profile from "store/reducers/profile";

const rootReducer = combineReducers({
	githubProfiles,
	companies,
	linkedinProfiles,
	jobs,
	profile,
});

export default rootReducer;