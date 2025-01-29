import Image from 'next/image'
import IterationIcon from './IterationIcon'
import { ImageType } from '@/app/types/imagetype'

interface ImageViewProps {
  image: ImageType,
  onInteraction?: (liked: boolean, imageId: number) => void;
}

const ImageView:React.FC<ImageViewProps> = ({image, onInteraction}) => {
  const { id, title, description, url, likeCount } = image;
  
  const handleClickIteration = (liked: boolean) => {
    if(onInteraction) {
      onInteraction(liked, id)
    }
  }

  return (
    <>
      <div id={`id-image-${ id }`} className="w-full">
        <div className="flex justify-between">
          <strong>{ title }</strong>
          <small>{ likeCount } likes</small>
        </div>
        <p className="text-sm text-justify text-slate-600">{ description }</p>
      </div>
      <Image
        src={ url } 
        alt="Image Title"
        width={600}
        height={400}
      />
      <div className="w-full justify-center flex gap-4 -translate-y-11">
        <IterationIcon icon="cancel" onClick={() => handleClickIteration(false)} />
        <IterationIcon icon="favourite" onClick={() => handleClickIteration(true)} />
      </div>
    </>
  )
}

export default ImageView;