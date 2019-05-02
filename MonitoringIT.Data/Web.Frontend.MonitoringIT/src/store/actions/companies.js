import types from "store/types";
// presentation data
export const requestFavoriteCompanies = count => ({ type: types.REQUESTED_FAVORITE_COMPANIES, count });
export const succeededFavoriteCompanies = profiles => ({ type: types.SUCCEEDED_FAVORITE_COMPANIES, profiles });
export const failedFavoriteCompanies = error => ({ type: types.FAILED_FAVORITE_COMPANIES, error });
