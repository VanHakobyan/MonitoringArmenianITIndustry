import { takeLatest } from "redux-saga/effects";
import types from "store/types";

import { getAllGithubProfiles, getFavoriteGithubProfiles } from "./githubProfiles";
import { getFavoriteLinkedinProfiles } from "./linkedinProfiles";
import { getFavoriteCompanies } from "./companies";

export default function* root() {
	yield takeLatest(types.REQUESTED_ALL_GITHUB_PROFILES, getAllGithubProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_GITHUB_PROFILES, getFavoriteGithubProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_LINKEDIN_PROFILES, getFavoriteLinkedinProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_COMPANIES, getFavoriteCompanies);
}