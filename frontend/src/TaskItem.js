export default function TaskItem({taskItem}) {
  return (
    <span className="has-hover-show">
      <label className="checkbox">
        <input type="checkbox" />
        &nbsp;{taskItem.description}
      </label>
      <button className="button is-white is-small ml-1 mb-2 is-invisible hover-show" title="Edit description">
        <span className="icon">
          <i className="fa-solid fa-pen-to-square"></i>
        </span>
      </button>
      <button className="button is-warning is-light is-small ml-1 mb-2 is-invisible hover-show" title="Permanently delete task">
        <span className="icon">
          <i className="fa-solid fa-circle-minus"></i>
        </span>
      </button>
    </span>
  );
}
