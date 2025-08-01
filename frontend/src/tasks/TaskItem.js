export default function TaskItem({taskItem, onEdit, onDelete}) {
  return (
    <span className="has-hover-show">
      <LabeledCheckBox taskItem={taskItem} onEdit={onEdit} />
      <EditButton taskItem={taskItem} onEdit={onEdit} />
      <DeleteButton taskItem={taskItem} onDelete={onDelete} />
    </span>
  );
}

function LabeledCheckBox({taskItem, onEdit}) {
  const handleOnChange = () => onEdit({...taskItem, isDone: !taskItem.isDone});

  return (
    <label className="checkbox">
      <input checked={taskItem.isDone} onChange={handleOnChange} type="checkbox" />
      <span className="ml-1">{taskItem.description}</span>
    </label>
  );
}

function EditButton({taskItem, onEdit}) {
  const handleOnClick = () => {
    const newDescription = window.prompt('New Description:', taskItem.description);
    if (newDescription !== null) {
      onEdit({...taskItem, description: newDescription});
    }
  }

  return (
    <button className="button is-white is-small ml-1 mb-2 is-invisible hover-show"
            onClick={handleOnClick}
            title="Edit description">
      <span className="icon">
        <i className="fa-solid fa-pen-to-square"></i>
      </span>
    </button>
  );
}

function DeleteButton({taskItem, onDelete}) {
  const handleOnClick = () => {
    if (window.confirm('Are you sure you would like to permanently delete the following task: \n\n' + taskItem.description))
      onDelete(taskItem.id);
  }

  return (
    <button className="button is-warning is-light is-small ml-1 mb-2 is-invisible hover-show"
      onClick={handleOnClick}
      title="Permanently delete task">
      <span className="icon">
        <i className="fa-solid fa-circle-minus"></i>
      </span>
    </button>
  );
}
