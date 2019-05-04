import types from "store/types";

// All
export const requestAllGithubProfiles = () => ({ type: types.REQUESTED_ALL_GITHUB_PROFILES });
export const succeededAllGithubProfiles = profiles => ({ type: types.SUCCEEDED_ALL_GITHUB_PROFILES, profiles });
export const failedAllGithubProfiles = error => ({ type: types.FAILED_ALL_GITHUB_PROFILES, error });

// presentation data
export const requestFavoriteGithubProfiles = count => ({ type: types.REQUESTED_FAVORITE_GITHUB_PROFILES, count });
export const succeededFavoriteGithubProfiles = profiles => ({ type: types.SUCCEEDED_FAVORITE_GITHUB_PROFILES, profiles });
export const failedFavoriteGithubProfiles = error => ({ type: types.FAILED_FAVORITE_GITHUB_PROFILES, error });

//page
export const requestGithubProfilesByPage = () => ({ type: types.REQUESTED_GITHUB_PROFILES_BY_PAGE });
export const succeededGithubProfilesByPage = profiles => ({ type: types.SUCCEEDED_GITHUB_PROFILES_BY_PAGE, profiles });
export const failedGithubProfilesByPage = error => ({ type: types.FAILED_GITHUB_PROFILES_BY_PAGE, error });
