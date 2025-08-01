export default function ErrorMessage({message}) {
  if (message) {
    return (
      <div className="notification is-warning is-light">
        {message}
      </div>
    );
  }
}

