import { takeLatest } from "redux-saga/effects";
import types from "store/types";

import { getAllGithubProfiles, getFavoriteGithubProfiles } from "./githubProfiles";
import { getFavoriteLinkedinProfiles,getLinkedinProfilesByPage } from "./linkedinProfiles";
import { getFavoriteCompanies,getCompaniesByPage } from "./companies";
import { getJobsByPage } from "./jobs";

export default function* root() {

	yield takeLatest(types.REQUESTED_JOBS_BY_PAGE, getJobsByPage);
	yield takeLatest(types.REQUESTED_COMPANIES_BY_PAGE, getCompaniesByPage);
	yield takeLatest(types.REQUESTED_ALL_GITHUB_PROFILES, getAllGithubProfiles);
	yield takeLatest(types.REQUESTED_LINKEDIN_PROFILES_BY_PAGE, getLinkedinProfilesByPage);
	yield takeLatest(types.REQUESTED_FAVORITE_GITHUB_PROFILES, getFavoriteGithubProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_LINKEDIN_PROFILES, getFavoriteLinkedinProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_COMPANIES, getFavoriteCompanies);
}