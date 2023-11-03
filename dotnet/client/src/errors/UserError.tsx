type ErrorName = 'GET_USERS_ERROR';

export default class UsersError extends Error {
  cause?: unknown;
  isInformative: boolean;
  message: string;
  name: ErrorName;
  status: number;

  constructor({
    cause,
    isInformative,
    message,
    name,
    status
  } : {
    cause?: unknown;
    isInformative: boolean;
    message: string;
    name: ErrorName;
    status: number;
  }) {
    super(message);
    this.cause = cause;
    this.isInformative = isInformative;
    this.message = message;
    this.name = name;
    this.status = status;
  }
};
