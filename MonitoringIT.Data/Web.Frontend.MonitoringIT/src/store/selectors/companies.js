export const favoriteCompaniesLoadingSelector = state => state.companies.favoriteCompaniesLoading;
export const favoriteCompaniesSuccessSelector = state => state.companies.favoriteCompaniesSuccess;
export const favoriteCompaniesFailedSelector = state => state.companies.favoriteCompaniesFailed;

export const companiesLoadingSelector = state => state.companies.companiesLoading;
export const companiesSuccessSelector = state => state.companies.companiesSuccess;
export const companiesFailedSelector = state => state.companies.companiesFailed;

export const currentCompaniesPageSelector = state => state.companies.currentCompaniesPage;