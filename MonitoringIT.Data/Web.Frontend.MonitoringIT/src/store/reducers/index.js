import { combineReducers } from "redux";
import githubProfiles from "store/reducers/githubProfiles";
import linkedinProfiles from "store/reducers/linkedinProfiles";
import companies from "store/reducers/companies";
import jobs from "store/reducers/jobs";

const rootReducer = combineReducers({
	githubProfiles,
	companies,
	linkedinProfiles,
	jobs
});

export default rootReducer;